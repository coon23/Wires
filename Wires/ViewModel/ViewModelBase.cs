using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wires.ViewModel
{
    /// <summary>
    /// Base class for all ViewModel classes.
    /// </summary>
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
