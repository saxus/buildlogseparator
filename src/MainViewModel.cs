﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildLogSeparator
{
    public class Region
    {
        public Region(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; }
        public string Content { get; }

        public override string ToString() => Name;
    }

    public class MainViewModel : INotifyPropertyChanged
    {


        private string _filename;
        public string Filename
        {
            get => _filename;
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    RaisePropertyChanged(nameof(Filename));
                }
            }
        }


        private List<Region> _regions;
        public List<Region> Regions
        {
            get => _regions;
            set
            {
                if (_regions != value)
                {
                    _regions = value;
                    RaisePropertyChanged(nameof(Regions));
                }
            }
        }


        private Region _selectedRegion;
        public Region SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                if (_selectedRegion != value)
                {
                    _selectedRegion = value;
                    RaisePropertyChanged(nameof(SelectedRegion));

                    Content = value?.Content;
                }
            }
        }







        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    RaisePropertyChanged(nameof(Content));
                }
            }
        }

        public ICommand OpenLogCommand { get; }

        public MainViewModel()
        {
            Filename = "Build log separator";
            Content = "Ctrl + O - Open file" + Environment.NewLine + "Alt + F4 - Exit";

            OpenLogCommand = new DelegateCommand((param) =>
            {
                var ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.Filter = "Logfiles|*.txt;*.log|All files|*.*";

                var res = ofd.ShowDialog();

                if (res ?? false)
                {
                    OpenLog(ofd.FileName);
                }
            });
        }

        public void OpenLog(string filename)
        {
            this.Filename = filename;

            var list = new List<Region>();

            var currentRegion = "main";
            var sb = new StringBuilder();

            var lines = File.ReadAllLines(filename);

            foreach (var line in lines)
            {
                if (line.Length > 30)
                {
                    var t = line.Substring(29);
                    if (t.StartsWith("======== ") && t.EndsWith(" logs"))
                    {
                        Swap();

                        currentRegion = t.Substring(9, t.Length - 9 - 5);                       
                        sb.Clear();
                    }
                }

                sb.AppendLine(line);
            }

            Swap();

            Regions = list;

            void Swap()
            {
                var region = new Region(currentRegion, sb.ToString());
                list.Add(region);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}