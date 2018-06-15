using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManagement.UI.Extensions
{
    public static class ObservableCollectionExtentions
    {
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, List<T> range) where T : class
        {
            foreach (var item in range)
            {
                observableCollection.Add(item);
            }
        }
    }
}
