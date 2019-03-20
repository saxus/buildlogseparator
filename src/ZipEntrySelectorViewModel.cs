using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Compression;

namespace BuildLogSeparator
{
    public class ZipEntrySelectorViewModel :INotifyPropertyChanged
    {

        private ZipArchiveEntry _selectedEntry;
        public ZipArchiveEntry SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                if (_selectedEntry != value)
                {
                    _selectedEntry = value;
                    RaisePropertyChanged(nameof(SelectedEntry));
                    RaisePropertyChanged(nameof(CanOpen));
                }
            }
        }

        public bool CanOpen => SelectedEntry != null;

        public IEnumerable<ZipArchiveEntry> Entries { get; }

        public ZipEntrySelectorViewModel(ZipArchive zipFile)
        {
            Entries = zipFile.Entries;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
