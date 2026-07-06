using System;
using System.Collections;
using SimPe.Interfaces.Plugin.Scanner;

namespace SimPe.Plugin.CollWsp
{
  public class CollWspScannerCollection : System.Collections.IEnumerable, System.IDisposable
  {
    ArrayList list;
		internal CollWspScannerCollection()
		{
			list = new ArrayList();
		}

		public virtual void Add(IScannerPluginBase item)
		{
			if (item==null) return;
			list.Add(item);
		}

		public int Count
		{
			get {return list.Count;}
		}

		public bool Contains(IScannerPluginBase item)
		{
			return list.Contains(item);
		}

		public void Sort(System.Collections.IComparer cmp)
		{
			list.Sort(cmp);
		}

		internal void Clear()
		{
			list.Clear();
		}

		public IScannerPluginBase this[int index]
		{
			get {return list[index] as IScannerPluginBase;}
		}

		#region IEnumerable Member

		public System.Collections.IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			if (list!=null) list.Clear();
			list = null;
		}

		#endregion
	}
}

