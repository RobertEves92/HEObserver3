using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HertfordshireMercury.Controls
{
    public class AdControlView : Xamarin.Forms.View
    {
        public string AdUnitId { get; set; }

        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
            propertyName: "AdUnitId",
            returnType: typeof(string),
            declaringType: typeof(AdControlView),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: AdUnitIdPropertyChanged);

        private static void AdUnitIdPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdControlView)bindable;
            control.AdUnitId = newValue.ToString();
            UnitId = newValue.ToString();
        }

        public static string UnitId
        {
            get;
            set;
        }
    }
}
