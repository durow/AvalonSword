/*
Author:durow
Date:2015.10.11
implement INotifyPropertyChanged interface
if property changed,can use RaisePropertyChanged method to Notify property changed
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Ayx.AvalonSword.MVVM
{
    public abstract class NotificationObject:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise notify when property value changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raise notify when property value changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var name = GetPropertyNameFromExpression(propertyExpression);
            RaisePropertyChanged(name);
        }

        /// <summary>
        /// if property value changed set property value and raise notify
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="propertyName">property name</param>
        /// <param name="oldValue">property old value</param>
        /// <param name="newValue">property new value</param>
        protected virtual bool SetAndNotify<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
                return false;

            oldValue = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        private static string GetPropertyNameFromExpression<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var memberExpression = FindMemberExpression(expression);
            if (memberExpression == null)
            {
                throw new ArgumentException("wrong expression", nameof(expression));
            }
            var member = memberExpression.Member as PropertyInfo;
            if (member == null)
            {
                throw new ArgumentException("wrong expression", nameof(expression));
            }
            if (member.DeclaringType == null)
            {
                throw new ArgumentException("wrong expression", nameof(expression));
            }

            if (member.GetGetMethod(true).IsStatic)
            {
                throw new ArgumentException("wrong expression", nameof(expression));
            }
            return member.Name;
        }
        private static MemberExpression FindMemberExpression<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as UnaryExpression;
            if (body != null)
            {
                var unary = body;
                var member = unary.Operand as MemberExpression;
                if (member == null)
                    throw new ArgumentException("must be unary expression", nameof(expression));
                return member;
            }
            return expression.Body as MemberExpression;
        }
    }
}
