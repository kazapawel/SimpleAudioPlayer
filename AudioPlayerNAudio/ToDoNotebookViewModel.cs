using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Linq;

namespace ToDoAppWpf
{
    /// <summary>
    /// View model of ToDoNotebook. Maybe change this facade patternt to real mvvm. Model should be facadeModel not model.
    /// </summary>
    public class ToDoNotebookViewModel : BaseViewModel
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// Model.
        /// </summary>
        private ToDoNotebook model;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Observable collection of todo tasks view models binded with Listbox.
        /// </summary>
        public ObservableCollection<ToDoViewModel> ToDoListObservable { get; set; }

        /// <summary>
        /// ToDo view model connected to controls from view.
        /// </summary>
        public ToDoViewModel BufferToDo { get; set; }

        /// <summary>
        /// Number of items in list.
        /// </summary>
        public string ItemsCount => $"( {ToDoListObservable.Count.ToString()} )";

        #endregion

        #region COMMANDS

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddToDoCommand { get; set; }

        public ICommand ClearAllDoneCommand { get; set; }

        public ICommand SelectAllItemsCommand { get; set; }
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ToDoNotebookViewModel()
        {
            //Creates model
            model = new ToDoNotebook(new TextFileManager(@"D:\ToDoTxtFile.txt", new ToDoToStringConverter()));

            //Inits observable collection
            ToDoListObservable = new ObservableCollection<ToDoViewModel>();

            //Inits viewmodel object connected to view controls
            BufferToDo = new ToDoViewModel(new ToDo("", DateTime.Now, Priority.Normal));

            //Commands
            AddToDoCommand = new RelayCommand(AddToDo);
            ClearAllDoneCommand = new RelayCommand(ClearAllDone,CanItemsBeCleard);
            SelectAllItemsCommand = new RelayCommand(SelectAllItems,CanSelectAllItems);

            //Loads data from model to observable collection
            LoadDataFromModel();

            //Subscribes to Collectionchanged event
            ToDoListObservable.CollectionChanged += SaveDataToModel;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Loads data from model to observable collection.
        /// </summary>
        private void LoadDataFromModel()
        {
            //Creates new  ToDo View Model
            foreach (var todo in model.ToDoList)
                ToDoListObservable.Add(new ToDoViewModel(todo));

            //Subscribes to all vm's events
            foreach (var vm in ToDoListObservable)
            {
                vm.OnRemoveClicked += RemoveToDo;
                vm.OnEditClicked += EditToDo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDataToModel(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                //Adding to observable collection
                case NotifyCollectionChangedAction.Add:

                    //New, last added VM item
                    var newToDoVm = (ToDoViewModel)e.NewItems[0];

                    //Creates new model object from new VM
                    if (newToDoVm != null)
                        //model.AddTodo(new ToDo(newToDoVm.Title, newToDoVm.FinishDate, newToDoVm.Priority));
                        model.AddTodo(newToDoVm.GetModel());
                    OnPropertyChanged(nameof(ItemsCount));
                    break;

                //Removing from observable collection
                case NotifyCollectionChangedAction.Remove:

                    //
                    var removedToDoVm = (ToDoViewModel)e.OldItems[0];
                    //
                    if (removedToDoVm != null)
                        model.RemoveTodo(removedToDoVm.GetModel());
                    OnPropertyChanged(nameof(ItemsCount));

                    break;
            }
        }

        /// <summary>
        /// Adds to do to collection.
        /// </summary>
        private void AddToDo(object o)
        {
            //Inserts new todo to observable collection at index 0
            ToDoListObservable.Insert(0, new ToDoViewModel(BufferToDo.Title, BufferToDo.FinishDate, BufferToDo.Priority));

            //Subscribes to events in created item
            ToDoListObservable[0].OnRemoveClicked += RemoveToDo;
            ToDoListObservable[0].OnEditClicked += EditToDo;

            //Clears buffer to do
            //BufferToDo = new ToDoViewModel(new ToDo("testujemy", DateTime.Now, Priority.Normal));
            BufferToDo.Title = string.Empty;
            BufferToDo.FinishDate = DateTime.Now;
            BufferToDo.Priority = Priority.Normal;
        }

        /// <summary>
        /// Removes todo from observable collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveToDo(object sender, EventArgs e) => ToDoListObservable.Remove((ToDoViewModel)sender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditToDo(object sender, EventArgs e) => ToDoListObservable.Remove((ToDoViewModel)sender);

        /// <summary>
        /// Removes all items from observable collection which are done
        /// </summary>
        private void ClearAllDone(object o)
        {
            foreach(var item in ToDoListObservable.ToList())
            {
                if (item.IsDone)
                    ToDoListObservable.Remove(item);
            }
        }

        /// <summary>
        /// Can execute predicate
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool CanItemsBeCleard(object o)
        {
            return ToDoListObservable.Count > 0;
        }

        /// <summary>
        /// Sets all items in observable collection as done.
        /// </summary>
        private void SelectAllItems(object o)
        {
            foreach (var item in ToDoListObservable)
                item.IsDone = true;
        }

        /// <summary>
        /// Can execute predicate.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool CanSelectAllItems(object o)
        {
            return ToDoListObservable.Count > 0;
        }

        #endregion
    }
}
