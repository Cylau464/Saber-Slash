using System.Collections.Generic;
using System;

namespace Engine.DI
{
    public static class DIContainer
    {
        private static class ObjectsPovider<TType>
        {
            internal static List<TType> Values = new List<TType>();
        }

        /// <summary>
        /// Here we register just one value, Always override to the last VALUE added.
        /// </summary>
        /// <typeparam name="T"> The Data Type that we will saved. </typeparam>
        public static void RegisterAsSingle<T>(T value)
        {
            if (value == null) throw new NullReferenceException("eventObject has a null value!...");

            ObjectsPovider<T>.Values.Clear();
            ObjectsPovider<T>.Values.Add(value);
        }

        /// <summary>
        /// Register the value in the list with the other values of the same type (TType). if the value already exist, It will not added.
        /// </summary>
        public static void Register<TType>(TType value)
        {
            ObjectsPovider<TType>.Values.Add(value);
        }

        /// <summary>
        /// Register range of values in the list with the other values with the same type (TType).
        /// </summary>
        public static void RegisterRange<TType>(IEnumerable<TType> collection)
        {
            if (collection == null) throw new ArgumentNullException();

            ObjectsPovider<TType>.Values.AddRange(collection);
        }

        /// <summary>
        /// Get the last object added non null of type T.
        /// </summary>
        /// <typeparam name="TSource"> The Data Type that we will saved. </typeparam>
        /// <typeparam name="TResult"> The type of object that we will return. </typeparam>
        public static TResult AsSingle<TSource, TResult>()
        {
            List<TSource> values = ObjectsPovider<TSource>.Values;
            for (int i = values.Count - 1; 0 <= i; i++)
            {
                object value = values[i];
                if (value != null && !value.Equals(null) && value is TResult) return (TResult)value;
            }

            return default(TResult);
        }

        /// <summary>
        /// Get the last object added non null of type T.
        /// </summary>
        /// <typeparam name="TSource"> The Data Type that we will saved. </typeparam>
        public static TSource AsSingle<TSource>()
        {
            List<TSource> values = ObjectsPovider<TSource>.Values;
            for (int i = values.Count - 1; 0 <= i; i++)
            {
                if (values[i] != null && !values[i].Equals(null)) return values[i];
            }

            return default(TSource);
        }

        /// <summary>
        /// To a list all values of type TType and return it as an array of TType.
        /// </summary>
        /// <typeparam name="T"> The Data Type that we will searching. </typeparam>
        public static IList<TType> Collect<TType>()
        {
            return ObjectsPovider<TType>.Values;
        }

        /// <summary>
        /// ToArray all values of type TType and return it as an array of TType.
        /// </summary>
        /// <typeparam name="T"> The Data Type that we will searching. </typeparam>
        public static TType[] ToArray<TType>()
        {
            return ObjectsPovider<TType>.Values.ToArray();
        }

        /// <summary>
        /// Clear all values of type (TType).
        /// </summary>
        public static void Clear<TType>()
        {
            ObjectsPovider<TType>.Values.Clear();
        }
    }
}
