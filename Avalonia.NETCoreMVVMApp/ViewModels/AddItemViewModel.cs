using System.Reactive;
using Avalonia.NETCoreMVVMApp.Models;
using ReactiveUI;

namespace Avalonia.NETCoreMVVMApp.ViewModels
{
    class AddItemViewModel : ViewModelBase
    {
        string description;
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public AddItemViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x));

            Ok = ReactiveCommand.Create(
                () => new TodoItem { Description = Description }, 
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
    }
}