using System.ComponentModel;

namespace BenJFT.Locksley.App.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged {
    public abstract event PropertyChangedEventHandler? PropertyChanged;
}