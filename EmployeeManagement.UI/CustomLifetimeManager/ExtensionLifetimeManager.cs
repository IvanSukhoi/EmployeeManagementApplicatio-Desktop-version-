using System;
using System.Runtime.Remoting.Lifetime;
using Unity.Lifetime;

namespace EmployeeManagement.UI.CustomLifetimeManager
{
    //public partial class ExtensionLifetimeManager :
    //    LifetimeManager, IDisposable
    //{
    //    private object value;
    //    private readonly ILease lease;
    //    public CacheLifetimeManager(ILease lease)
    //    {
    //        if (lease == null)
    //        {
    //            throw new ArgumentNullException("lease");
    //        }
    //        this.lease = lease;
    //    }
    //    public override object GetValue()
    //    {
    //        this.RemoveValue();
    //        return this.value;
    //    }
    //    public override void RemoveValue()
    //    {
    //        if (this.lease.IsExpired)
    //        {
    //            this.Dispose();
    //        }
    //    }
    //    public override void SetValue(object newValue)
    //    {
    //        this.value = newValue;
    //        this.lease.Renew();
    //    }
    //
    //}
}
