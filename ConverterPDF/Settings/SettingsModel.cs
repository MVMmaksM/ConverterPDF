using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class SettingsModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _pathFolderLogs;
        private string _pathAbout;
        private string _nameUnitePdf;
        private int _indexFolderOpenFile;
        private int _indexIsVisibleExcel;
        private int _indexIsVisibleWord;
        private int _indexFolderSavePdf;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "PathFolderLogs":
                        if (PathFolderLogs is null)
                        {
                            error = "Необходимо указать путь";
                        }
                        break;
                    case "PathAbout":
                        if (PathAbout is null)
                        {
                            error = "Необходимо указать путь";
                        }
                        break;
                    case "NameUnitePdf":
                        if (NameUnitePdf is null)
                        {
                            error = "Необходимо указать имя";
                        }
                        break;
                }
                return error;
            }
        }

        public string PathFolderLogs
        {
            get => _pathFolderLogs;
            set
            {
                _pathFolderLogs = value;
                OnPropertyChanged("PathFolderLogs");
            }
        }
        public string PathAbout
        {
            get { return _pathAbout; }
            set
            {
                _pathAbout = value;
                OnPropertyChanged("PathAbout");
            }
        }
        public string NameUnitePdf
        {
            get { return _nameUnitePdf; }
            set
            {
                _nameUnitePdf = value;
                OnPropertyChanged("NameUnitePdf");
            }
        }
        public int IndexFolderOpenFile
        {
            get { return _indexFolderOpenFile; }
            set
            {
                _indexFolderOpenFile = value;
                OnPropertyChanged("IndexFolderOpenFile");
            }
        }
        public int IndexIsVisibleExcel
        {
            get { return _indexIsVisibleExcel; }
            set
            {
                _indexIsVisibleExcel = value;
                OnPropertyChanged("IndexIsVisibleExcel");
            }
        }
        public int IndexIsVisibleWord
        {
            get { return _indexIsVisibleWord; }
            set
            {
                _indexIsVisibleWord = value;
                OnPropertyChanged("IndexIsVisibleWord");
            }
        }
        public int IndexFolderSavePdf
        {
            get { return _indexFolderSavePdf; }
            set
            {
                _indexFolderSavePdf = value;
                OnPropertyChanged("IndexFolderSavePdf");
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
