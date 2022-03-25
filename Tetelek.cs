using System;
using System.Collections.Generic;

namespace ProgTetelek
{
    public static class Tetelek
    {
        static InvalidOperationException NoElementsException => new InvalidOperationException("Sequence contains no elements");
        static void CheckForAnyElemets<T>(IEnumerable<T> collection)
        {
            if (!collection.GetEnumerator().MoveNext())
                throw NoElementsException;
        }

        public static bool All<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (!predicate(item))
                    return false;
            return true;
        }

        public static bool Any<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (predicate(item))
                    return true;
            return false;
        }

        public static bool Contains<T>(this IEnumerable<T> collection, T element) where T : IComparable<T>
        {
            Comparer<T> comparer = Comparer<T>.Default;
            foreach (T item in collection)
                if (comparer.Compare(item, element) == 0)
                    return true;
            return false;
        }

        public static int Count<T>(this IEnumerable<T> collection)
        {
            int c = 0;
            foreach (T item in collection)
                c++;
            return c;
        }

        public static int Count<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            int c = 0;
            foreach (T item in collection)
                if (predicate(item))
                    c++;
            return c;
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> collection)
        {
            List<T> elements = new List<T>();
            foreach (T item in collection)
                if (!elements.Contains(item))
                    elements.Add(item);
            return elements;
        }

        public static T ElementAt<T>(this IEnumerable<T> collection, int index)
        {
            int i = 0;
            foreach (T item in collection)
                if (i++ == index)
                    return item;
            throw NoElementsException;
        }

        public static T First<T>(this IEnumerable<T> collection)
        {
            IEnumerator<T> enumerator = collection.GetEnumerator();
            if (enumerator.MoveNext())
                return enumerator.Current;
            throw NoElementsException;
        }

        public static T First<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (predicate(item))
                    return item;
            throw NoElementsException;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> collection)
        {
            IEnumerator<T> enumerator = collection.GetEnumerator();
            if (enumerator.MoveNext())
                return enumerator.Current;
            return default;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (predicate(item))
                    return item;
            return default;
        }

        public static T Last<T>(this IEnumerable<T> collection)
        {
            CheckForAnyElemets(collection);
            IEnumerator<T> enumerator = collection.GetEnumerator();
            enumerator.MoveNext();
            T ret = enumerator.Current;
            while (enumerator.MoveNext())
                ret = enumerator.Current;
            return ret;
        }

        public static T Last<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            collection = collection.Where(predicate);
            CheckForAnyElemets(collection);
            IEnumerator<T> enumerator = collection.GetEnumerator();
            enumerator.MoveNext();
            T ret = enumerator.Current;
            while (enumerator.MoveNext())
                ret = enumerator.Current;
            return ret;
        }

        public static T Max<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            CheckForAnyElemets(collection);
            Comparer<T> comparer = Comparer<T>.Default;
            T max = collection.First();
            foreach (T item in collection.Skip(1))
                if (comparer.Compare(item, max) > 0)
                    max = item;
            return max;
        }

        public static T Max<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T : IComparable<T>
        {
            CheckForAnyElemets(collection);
            Comparer<T> comparer = Comparer<T>.Default;
            T max = collection.First();
            foreach (T item in collection.Skip(1))
                if (comparer.Compare(item, max) > 0 && predicate(item))
                    max = item;
            return max;
        }

        public static T Min<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            CheckForAnyElemets(collection);
            Comparer<T> comparer = Comparer<T>.Default;
            T min = collection.First();
            foreach (T item in collection.Skip(1))
                if (comparer.Compare(item, min) < 0)
                    min = item;
            return min;
        }

        public static T Min<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T : IComparable<T>
        {
            CheckForAnyElemets(collection);
            Comparer<T> comparer = Comparer<T>.Default;
            T min = collection.First();
            foreach (T item in collection.Skip(1))
                if (comparer.Compare(item, min) < 0 && predicate(item))
                    min = item;
            return min;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            QuickSort(ref array, 0, array.Length - 1);
            return array;
        }

        public static IEnumerable<K> OrderBy<T, K>(this IEnumerable<T> collection, Func<T, K> selector) where K : IComparable<K>
        {
            K[] array = collection.Select(selector).ToArray();
            QuickSort(ref array, 0, array.Length - 1);
            return array;
        }

        public static IEnumerable<T> OrderByDesc<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            QuickSort(ref array, 0, array.Length - 1);
            return array.Reverse();
        }

        public static IEnumerable<K> OrderByDesc<T, K>(this IEnumerable<T> collection, Func<T, K> selector) where K : IComparable<K>
        {
            K[] array = collection.Select(selector).ToArray();
            QuickSort(ref array, 0, array.Length - 1);
            return array.Reverse();
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> collection)
        {
            T[] array = new T[collection.Count()];
            int i = array.Length - 1;
            foreach (T item in collection)
                array[i--] = item;
            return array;
        }

        public static IEnumerable<K> Select<T, K>(this IEnumerable<T> collection, Func<T, K> selector)
        {
            foreach (T item in collection)
                yield return selector(item);
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> collection, int number)
        {
            IEnumerator<T> enumerator = collection.GetEnumerator();
            for (int i = 0; i < number; i++)
                enumerator.MoveNext();
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> collection, int number)
        {
            IEnumerator<T> enumerator = collection.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < number)
                yield return enumerator.Current;
        }

        public static T Sum<T>(this IEnumerable<T> collection)
        {
            if (collection.Count() == 0)
                return default;
            dynamic sum = default(T);
            foreach (dynamic item in collection)
                sum += item;
            return sum;
        }

        public static K Sum<T, K>(this IEnumerable<T> collection, Func<T, K> selector)
        {
            if (collection.Count() == 0)
                return default;
            dynamic sum = default(K);
            foreach (dynamic item in collection.Select(selector))
                sum += item;
            return sum;
        }

        public static T[] ToArray<T>(this IEnumerable<T> collection)
        {
            return ToArray(collection, collection.Count());
        }

        public static T[] ToArray<T>(this IEnumerable<T> collection, int count)
        {
            T[] array = new T[count];
            int i = 0;
            foreach (T item in collection)
                array[i++] = item;
            return array;
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (predicate(item))
                    yield return item;
        }

        public static void QuickSort<T>(ref T[] array, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                Comparer<T> comparer = Comparer<T>.Default;
                T temp, pivot = array[high];

                int i = low - 1;
                for (int j = low; j < high; j++)
                    if (comparer.Compare(array[j], pivot) < 0)
                    {
                        temp = array[++i];
                        array[i] = array[j];
                        array[j] = temp;
                    }

                temp = array[high];
                array[high] = array[i + 1];
                array[i + 1] = temp;
                int pi = i + 1;

                QuickSort(ref array, low, pi - 1);
                QuickSort(ref array, pi + 1, high);
            }
        }
    }
}
