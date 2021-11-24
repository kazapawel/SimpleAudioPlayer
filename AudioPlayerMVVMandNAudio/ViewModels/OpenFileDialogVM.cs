using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayerMVVMandNAudio
{
    public class OpenFileDialogVM : BaseViewModel
    {
        #region PRIVATE MEMBERS

        private string selectedPath;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public string SelectedPath
        {
            get 
            {
                return selectedPath; 
            }
            set
            {
                if(selectedPath!=value)
                {
                    selectedPath = value;
                    OnPropertyChanged(nameof(SelectedPath));
                }
            }
        }

        /// <summary>
        /// Command for opening dialog box.
        /// </summary>
        public RelayCommand OpenFileCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public OpenFileDialogVM()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        private void OpenFile(object o)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
        }
    }
}
