using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace DataformXamarin
{
    public class DataformBehavior : Behavior<ContentPage>
    {
        SfDataForm dataForm;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            dataForm = bindable.FindByName<SfDataForm>("dataForm");
            dataForm.DataObject = new ContactInfo();
            dataForm.ItemManager = new DataFormItemManagerExt(dataForm);
        }
    }
    public class DataFormItemManagerExt : DataFormItemManager
    {
        SfDataForm sfDataForm;
        public DataFormItemManagerExt(SfDataForm dataForm) : base(dataForm)
        {
            sfDataForm = dataForm;
        }
        protected override List<DataFormItemBase> GenerateDataFormItems(PropertyInfoCollection itemProperties, List<DataFormItemBase> dataFormItems)
        {
            var items = new List<DataFormItemBase>();
            foreach (var propertyInfo in itemProperties)
            {
                DataFormItem dataFormItem;
                if (propertyInfo.Key == "ID")
                    dataFormItem = new DataFormNumericItem() { Name = propertyInfo.Key, Editor = "Numeric", MaximumNumberDecimalDigits = 0 };
                else if (propertyInfo.Key == "Name")
                    dataFormItem = new DataFormTextItem() { Name = propertyInfo.Key, Editor = "Text" };
                else
                    dataFormItem = new DataFormTextItem() { Name = propertyInfo.Key, Editor = "Text" };

                items.Add(dataFormItem);
            }

            return items;
        }
        public override object GetValue(DataFormItem dataFormItem)
        {
            var value = sfDataForm.DataObject.GetType().GetRuntimeProperty(dataFormItem.Name).GetValue(sfDataForm.DataObject);
            return value;
        }
        public override void SetValue(DataFormItem dataFormItem, object value)
        {
            sfDataForm.DataObject.GetType().GetRuntimeProperty(dataFormItem.Name).SetValue(sfDataForm.DataObject, value);
        }
    }
}




