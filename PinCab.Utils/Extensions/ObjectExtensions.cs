﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PinCab.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static void SetPropValue(this Object obj, String name, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name);
            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }
    }
}