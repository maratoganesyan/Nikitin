using Nikitin.Tools;
using Nikitin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nikitin.ViewModels;
internal class SomePagesViewModel<TTable> : ViewModelBase where TTable : class
{
    #region fields
    private ObservableCollection<TTable> _tableValue;

    private string _searchingText;

    #endregion

    #region props
    public ObservableCollection<TTable> TableValue
    {
        get => _tableValue;
        set
        {
            _tableValue = value;
            OnPropertyChanged(nameof(TableValue));
        }
    }

    public string SearchingText
    {
        get => _searchingText;
        set
        {
            _searchingText = value;
            OnPropertyChanged(nameof(SearchingText));
        }
    }

    #endregion

    public SomePagesViewModel()
    {
        Refresh();
    }


    #region Commands
    public RelayCommand SearchDataCommand => new RelayCommand(obj => SearchData(SearchingText));

    public RelayCommand OpenAddDialogCommand => new RelayCommand(obj => OpenAddDialog(obj));

    public RelayCommand OpenChangeDialogCommand => new RelayCommand(obj => OpenChangeDialog(obj));

    public RelayCommand RefreshCommand => new RelayCommand(obj => Refresh());

    #endregion

    #region Methods
    protected virtual void Refresh()
    {
        try
        {
            var values = DbUtils.GetTableAllValues<TTable>();
            TableValue = new ObservableCollection<TTable>(values);
        }
        catch (Exception ex)
        {
            CustomMessageBox.Show("Ошибка запроса к серверу");
        }

    }

    protected virtual void SearchData(string searchText)
    {
        try
        {
            var values = DbUtils.GetSearchingValues<TTable>(searchText);
            TableValue = new ObservableCollection<TTable>(values);
        }
        catch (Exception ex)
        {
            CustomMessageBox.Show("Ошибка запроса к серверу");
        }
    }

    private void OpenAddDialog(object parameter)
    {
        if (parameter is Type userControlType && typeof(Window).IsAssignableFrom(userControlType))
        {
            try
            {
                var window = (Window)Activator.CreateInstance(userControlType);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Ошибка при открытии страницы");
            }
        }
        else
        {

        }
    }

    private void OpenChangeDialog(object parameter)
    {
        if (parameter is object[] parameters && parameters.Length == 2)
        {
            if (parameters[1] is Type userControlType && typeof(Window).IsAssignableFrom(userControlType) && parameters[0] is Button button)
            {
                ConstructorInfo constructor = userControlType.GetConstructor(new Type[] { typeof(TTable) });
                if (constructor != null)
                {
                    TTable value = (TTable)button.DataContext;
                    var window = (Window)constructor.Invoke(new object[] { value });
                    window.ShowDialog();
                }
                else
                {
                    CustomMessageBox.Show("Ошибка при открытии страницы");
                }
            }
        }

    }
    #endregion
}
