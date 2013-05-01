using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sluttprosjekt.Helpers
{
    public static class BindingHelper
    {
        
        public static void UpdateSource()
        {
            var element = FocusManager.GetFocusedElement();
            if (element is TextBox)
                UpdateTextboxBindingSource(element as TextBox);
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
