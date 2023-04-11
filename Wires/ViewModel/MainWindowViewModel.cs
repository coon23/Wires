using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Wires.Commands;
using Wires.Data;
using Wires.Helpers;
using Wires.Resources;


namespace Wires.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        #region field

        private string _dataFilePath;
        private ObservableCollection<Shape> _resultToShow;
        private Wire[] _wires;
        private Cable _cable;
        private double _scale = 10;
        private bool _canSetDataButtonExecute;


        #endregion


        #region properties

        public ICommand SetDataFileAndRunCommand { get; set; }

        public string DataFilePath
        {
            get { return _dataFilePath; }
            set
            {
                if(_dataFilePath == value)
                {
                    return;
                }
                _dataFilePath = value;                
                OnPropertyChanged(nameof(DataFilePath));
            }
        }

        public ObservableCollection<Shape> ResultToShow
        {
            get { return _resultToShow; }
            set
            {
                if(_resultToShow == value)
                {
                    return;
                }
                _resultToShow = value;
                OnPropertyChanged(nameof(ResultToShow));
            }
        }

        public Cable Cable
        {
            get { return _cable; }
            set
            {
                if (_cable == value)
                {
                    return;
                }

                _cable = value;
                OnPropertyChanged(nameof(Cable));

            }
        }

        #endregion


        public MainWindowViewModel()
        {
            SetDataFileAndRunCommand = new RelayCommand(OnSetDataButtonClick, (x) => _canSetDataButtonExecute);
            _resultToShow = new ObservableCollection<Shape>();
            _canSetDataButtonExecute = true;
        }

        /// <summary>
        /// Click on SetDataButton - loads data, compute, show results.
        /// </summary>
        /// <param name="parameter"></param>
        private async void OnSetDataButtonClick(object parameter)
        {
            _canSetDataButtonExecute = false;

            try
            {
                ResultToShow.Clear();

                await Task.Run(LoadFileAndRun);

                if (Cable != null)
                {
                    DrawCable();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ErrorMessages.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _canSetDataButtonExecute = true;
            }
        }

        private void LoadFileAndRun()
        {
            FileDialog();
            if (DataFilePath == null)
            {
                return;
            }

            CreateWires();
            CalculateCable();
        }

        private void CalculateCable()
        {
            WireFit fit = new WireFit(_wires);
            fit.Fit();
            Cable = fit.Cable;            
        }

        /// <summary>
        /// Add cables to the collection to draw
        /// </summary>
        private void DrawCable()
        {
            foreach (Wire wire in _cable.Wires)
            {
                Shape wireGraphics = wire.CrossSectionVisual(_scale, Brushes.Green, 1, Brushes.GhostWhite);
                double setTopW = _scale * (((wire.Center?.Y ?? 0) - wire.Radius) +  _cable.Radius);
                double setLeftW = _scale * (((wire.Center?.X ?? 0) - wire.Radius) + _cable.Radius);
                ResultToShow.Add(DrawHelper.GetShapeToDraw(wireGraphics, setTopW, setLeftW));
            }

            Shape cableGraphics = _cable.CrossSectionVisual(_scale, Brushes.Red, 2, Brushes.Transparent);
            double setTopC = _scale * (((_cable.Center?.Y ?? 0) - _cable.Radius) + _cable.Radius);
            double setLeftC = _scale * (((_cable.Center?.X ?? 0) - _cable.Radius) + _cable.Radius);
            ResultToShow.Add(DrawHelper.GetShapeToDraw(cableGraphics, setTopC, setLeftC));
        }

        /// <summary>
        /// Opens file dialog window to get radii source file.
        /// </summary>
        private void FileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = $"{TextResources.TextFiles} (*.txt)|*.txt",
                Multiselect = false
            };

            if(openFileDialog.ShowDialog() == true)
            {
                DataFilePath = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Loads radii from given file and creates wires array.
        /// </summary>
        private void CreateWires()
        {
            double[] radii = InputHelper.GetValuesFromTxt(_dataFilePath).OrderByDescending(r => r).ToArray();
            _wires = new Wire[radii.Length];
            for (int i = 0; i < radii.Length; i++)
            {
                _wires[i] = new Wire(null, radii[i]); // centers are not known yet
                Trace.WriteLine(radii[i]);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        private void SetScale()
        {
            //_scale = ....
        }


        
    }
}
