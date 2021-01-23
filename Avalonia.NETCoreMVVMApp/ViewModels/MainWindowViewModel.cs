﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Avalonia.NETCoreMVVMApp.Models;
using Avalonia.NETCoreMVVMApp.Services;
using ReactiveUI;

namespace Avalonia.NETCoreMVVMApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        
        public MainWindowViewModel(Database db)
        {
            List = new TodoListViewModel(db.GetItems());
            Content = List;
        }

        public TodoListViewModel List { get; }
        
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(
                    vm.Ok,
                    vm.Cancel.Select(_ => (TodoItem)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        List.Items.Add(model);
                    }

                    Content = List;
                });

            Content = vm;
        }
    }
}