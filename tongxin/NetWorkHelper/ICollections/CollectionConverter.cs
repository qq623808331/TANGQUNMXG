﻿using NetWorkHelper.Helper;
using NetWorkHelper.ICommond;
using System;
using System.Collections.Generic;

namespace NetWorkHelper.ICollections
{
    /// <summary>
    /// CollectionConverter 用于转换集合内的元素或集合类型。
    /// </summary>
    public static class CollectionConverter
    {
        #region ConvertAll
        /// <summary>
        /// ConvertAll 将source中的每个元素转换为TResult类型
        /// </summary>       
        public static List<TResult> ConvertAll<TObject, TResult>(IEnumerable<TObject> source, CommonMethod.Func<TObject, TResult> converter)
        {
            return CollectionConverter.ConvertSpecification<TObject, TResult>(source, converter, null);
        }
        #endregion         

        #region ConvertSpecification
        /// <summary>
        /// ConvertSpecification 将source中的符合predicate条件元素转换为TResult类型
        /// </summary>       
        public static List<TResult> ConvertSpecification<TObject, TResult>(IEnumerable<TObject> source, CommonMethod.Func<TObject, TResult> converter, Predicate<TObject> predicate)
        {
            List<TResult> list = new List<TResult>();
            CollectionHelper.ActionOnSpecification<TObject>(source, delegate (TObject ele) { list.Add(converter(ele)); }, predicate);
            return list;
        }
        #endregion               

        #region ConvertFirstSpecification
        /// <summary>
        /// ConvertSpecification 将source中的符合predicate条件的第一个元素转换为TResult类型
        /// </summary>       
        public static TResult ConvertFirstSpecification<TObject, TResult>(IEnumerable<TObject> source, CommonMethod.Func<TObject, TResult> converter, Predicate<TObject> predicate)
        {
            TObject target = CollectionHelper.FindFirstSpecification<TObject>(source, predicate);

            if (target == null)
            {
                return default(TResult);
            }

            return converter(target);
        }
        #endregion       

        #region CopyAllToList
        public static List<TObject> CopyAllToList<TObject>(IEnumerable<TObject> source)
        {
            List<TObject> copy = new List<TObject>();
            CollectionHelper.ActionOnEach<TObject>(source, delegate (TObject t) { copy.Add(t); });
            return copy;
        }
        #endregion

        #region CopySpecificationToList
        public static List<TObject> CopySpecificationToList<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
        {
            List<TObject> copy = new List<TObject>();
            CollectionHelper.ActionOnSpecification<TObject>(source, delegate (TObject t) { copy.Add(t); }, predicate);
            return copy;
        }
        #endregion        

        #region ConvertListUpper
        /// <summary>
        /// ConvertListUpper 将子类对象集合转换为基类对象集合
        /// </summary>        
        public static List<TBase> ConvertListUpper<TBase, T>(IList<T> list) where T : TBase
        {
            List<TBase> baseList = new List<TBase>(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                baseList.Add(list[i]);
            }

            return baseList;
        }
        #endregion

        #region ConvertListDown
        /// <summary>
        /// ConvertListDown 将基类对象集合强制转换为子类对象集合
        /// </summary>        
        public static List<T> ConvertListDown<TBase, T>(IList<TBase> baseList) where T : TBase
        {
            List<T> list = new List<T>(baseList.Count);
            for (int i = 0; i < baseList.Count; i++)
            {
                list.Add((T)baseList[i]);
            }

            return list;
        }
        #endregion

        #region ConvertArrayToList
        /// <summary>
        /// ConverArrayToList 将数组转换为List
        /// </summary>      
        public static List<TElement> ConvertArrayToList<TElement>(TElement[] ary)
        {
            if (ary == null)
            {
                return null;
            }

            return CollectionHelper.Find<TElement>(ary, null);
        }
        #endregion

        #region ConvertListToArray
        /// <summary>
        /// ConverListToArray 将List转换为数组
        /// </summary>      
        public static TElement[] ConvertListToArray<TElement>(IList<TElement> list)
        {
            if (list == null)
            {
                return null;
            }

            TElement[] ary = new TElement[list.Count];
            for (int i = 0; i < ary.Length; i++)
            {
                ary[i] = list[i];
            }

            return ary;
        }
        #endregion

    }
}
