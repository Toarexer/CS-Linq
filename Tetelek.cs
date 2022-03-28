using System;
using System.Collections;
using System.Collections.Generic;

namespace ProgTetelek
{
    public static class Querry
    {
        static InvalidOperationException NoElementsException => new InvalidOperationException("Sequence contains no elements");
        static InvalidOperationException MultipleElementsException => new InvalidOperationException("Sequence contains more or less than one element");

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

        public static IEnumerable<T> Append<T>(this IEnumerable<T> collection, T item)
        {
            foreach (T original_item in collection)
                yield return original_item;
            yield return item;
        }

        public static T Avarage<T>(this IEnumerable<T> collection)
        {
            if (collection.Count() == 0)
                return default;
            dynamic sum = default(T);
            foreach (dynamic item in collection)
                sum += item;
            return sum / collection.Count();
        }

        public static IEnumerable<T> Cast<T>(this IEnumerable collection)
        {
            foreach (object item in collection)
                yield return (T)Convert.ChangeType(item, typeof(T));
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (T item in first)
                yield return item;
            foreach (T item in second)
                yield return item;
        }

        public static bool Contains<T>(this IEnumerable<T> collection, T element)
        {
            foreach (T item in collection)
                if (item.Equals(element))
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

        public static T ElementAtOrDefault<T>(this IEnumerable<T> collection, int index)
        {
            int i = 0;
            foreach (T item in collection)
                if (i++ == index)
                    return item;
            return default;
        }

        public static bool Empty<T>(this IEnumerable<T> collection)
        {
            return collection.Count() == 0;
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (!predicate(item))
                    yield return item;
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

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (T item in first)
                if (second.Contains(item))
                    yield return item;
        }

        public static T Last<T>(this IEnumerable<T> collection)
        {
            CheckForAnyElemets(collection);
            IEnumerator<T> enumerator = collection.GetEnumerator();
            enumerator.MoveNext();
            T last = enumerator.Current;
            while (enumerator.MoveNext())
                last = enumerator.Current;
            return last;
        }

        public static T Last<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            collection = collection.Where(predicate);
            CheckForAnyElemets(collection);
            IEnumerator<T> enumerator = collection.GetEnumerator();
            enumerator.MoveNext();
            T last = enumerator.Current;
            while (enumerator.MoveNext())
                last = enumerator.Current;
            return last;
        }

        public static T LastOrDefault<T>(this IEnumerable<T> collection)
        {
            T last = default;
            foreach (T item in collection)
                last = item;
            return last;
        }

        public static T LastOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            T last = default;
            foreach (T item in collection)
                if (predicate(item))
                    last = item;
            return last;
        }

        public static long LongCount<T>(this IEnumerable<T> collection)
        {
            long c = 0;
            foreach (T item in collection)
                c++;
            return c;
        }

        public static long LongCount<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            long c = 0;
            foreach (T item in collection)
                if (predicate(item))
                    c++;
            return c;
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

        public static IEnumerable<T> OfType<T>(this IEnumerable collection)
        {
            foreach (object item in collection)
                if (item is T)
                    yield return (T)Convert.ChangeType(item, typeof(T));
        }

        public static IEnumerable<T> Order<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            Sorting.QuickSort(ref array, 0, array.Length - 1);
            return array;
        }

        public static IEnumerable<T> OrderBy<T, A>(this IEnumerable<T> collection, Func<T, A> selector) where A : IComparable<A>
        {
            T[] array = collection.ToArray();
            Sorting.QuickSort(ref array, 0, array.Length - 1, selector);
            return array;
        }

        public static IEnumerable<T> OrderDescending<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            Sorting.QuickSort(ref array, 0, array.Length - 1);
            return array.Reverse();
        }

        public static IEnumerable<T> OrderByDescending<T, A>(this IEnumerable<T> collection, Func<T, A> selector) where A : IComparable<A>
        {
            T[] array = collection.ToArray();
            Sorting.QuickSort(ref array, 0, array.Length - 1, selector);
            return array.Reverse();
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> collection, T item)
        {
            yield return item;
            foreach (T original_item in collection)
                yield return original_item;
        }

        public static IEnumerable<T> Repeat<T>(T item, int count)
        {
            for (int i = 0; i < count; i++)
                yield return item;
        }

        public static IEnumerable<T> Repeat<T>(IEnumerable<T> collection, int count)
        {
            for (int i = 0; i < count; i++)
                foreach (T item in collection)
                    yield return item;
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> collection)
        {
            T[] array = new T[collection.Count()];
            int i = array.Length - 1;
            foreach (T item in collection)
                array[i--] = item;
            return array;
        }

        public static IEnumerable<A> Select<T, A>(this IEnumerable<T> collection, Func<T, A> selector)
        {
            foreach (T item in collection)
                yield return selector(item);
        }

        public static IEnumerable<A> SelectMany<T, A>(this IEnumerable<T> collection, Func<T, IEnumerable<A>> selector)
        {
            foreach (T item in collection)
                foreach (A inner_item in selector(item))
                    yield return inner_item;
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            IEnumerator enumerator1 = first.GetEnumerator();
            IEnumerator enumerator2 = second.GetEnumerator();
            while (true)
            {
                bool success1 = enumerator1.MoveNext();
                bool success2 = enumerator2.MoveNext();
                if (success1 ^ success2)
                    return false;
                if (!success1 && !success2)
                    return true;
                if (enumerator1.Current != enumerator2.Current)
                    return false;
            }
        }

        public static T Single<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 1)
                return source.First();
            throw MultipleElementsException;
        }

        public static T Single<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source.Count(predicate) == 1)
                return source.First(predicate);
            throw MultipleElementsException;
        }

        public static T SingleOrDefault<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 1)
                return source.First();
            throw default;
        }

        public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source.Count(predicate) == 1)
                return source.First(predicate);
            throw default;
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

        public static IEnumerable<T> SkipLastWhile<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
            {
                if (predicate(item))
                    yield break;
                yield return item;
            }
        }

        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            IEnumerator<T> enumerator = collection.GetEnumerator();
            while (true)
            {
                if (!enumerator.MoveNext())
                    yield break;
                if (!predicate(enumerator.Current))
                    break;
            }
            while (enumerator.MoveNext())
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

        public static A Sum<T, A>(this IEnumerable<T> collection, Func<T, A> selector)
        {
            if (collection.Count() == 0)
                return default;
            dynamic sum = default(A);
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
            IEnumerator<T> enumerator = collection.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i < count)
                array[i++] = enumerator.Current;
            return array;
        }

        public static List<T> ToList<T>(this IEnumerable<T> collection)
        {
            return new List<T>(collection);
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            List<T> items = new List<T>();
            foreach (T item in first)
                if (!items.Contains(item))
                    items.Add(item);
            foreach (T item in second)
                if (!items.Contains(item))
                    items.Add(item);
            return items;
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (T item in collection)
                if (predicate(item))
                    yield return item;
        }
    }

    public static class Sorting
    {
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

        public static void QuickSort<T, A>(ref T[] array, int low, int high, Func<T, A> selector) where A : IComparable<A>
        {
            if (low < high)
            {
                Comparer<A> comparer = Comparer<A>.Default;
                T temp;
                A pivot = selector(array[high]);

                int i = low - 1;
                for (int j = low; j < high; j++)
                    if (comparer.Compare(selector(array[j]), pivot) < 0)
                    {
                        temp = array[++i];
                        array[i] = array[j];
                        array[j] = temp;
                    }

                temp = array[high];
                array[high] = array[i + 1];
                array[i + 1] = temp;
                int pi = i + 1;

                QuickSort(ref array, low, pi - 1, selector);
                QuickSort(ref array, pi + 1, high, selector);
            }
        }
    }
}
