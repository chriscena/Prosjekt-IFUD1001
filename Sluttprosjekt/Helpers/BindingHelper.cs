using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace Sluttprosjekt.Helpers
{
    public static class BindingHelper
    {
        
        public static void UpdateSource()
        {
            var element = FocusManager.GetFocusedElement();
            if (element is TextBox)
                UpdateTextboxBindingSource(element as TextBox);
            if (element is ListPicker)
                UpdateListPickerBindingSource(element as ListPicker);
        }

        private static void UpdateListPickerBindingSource(ListPicker element)
        {
            if (element == null) return;

            var currentBinding = element.GetBindingExpression(ListPicker.SelectedItemProperty);
            if (currentBinding != null)
                currentBinding.UpdateSource();
        }

        private static void UpdateTextboxBindingSource(TextBox element)
        {
            if (element == null) return;

            var currentBinding = element.GetBindingExpression(TextBox.TextProperty);
            if (currentBinding != null)
                currentBinding.UpdateSource();
        }

    }
}
