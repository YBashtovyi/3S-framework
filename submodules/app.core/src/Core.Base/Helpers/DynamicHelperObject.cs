using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Core.Base.Helpers
{
    public class DynamicHelperObject: DynamicObject
    {
        private readonly Dictionary<string, object> _members = new Dictionary<string, object>();

        public object GetMember(string name)
        {
            var binder = Binder.GetMember(CSharpBinderFlags.None,
                  name, GetType(),
                  new List<CSharpArgumentInfo>{
                       CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)});
            var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);

            return callsite.Target(callsite, this);
        }

        public void SetMember(string name, object value)
        {
            var binder = Binder.SetMember(CSharpBinderFlags.None,
                   name, GetType(),
                   new List<CSharpArgumentInfo>{
                       CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                       CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)});
            var callsite = CallSite<Func<CallSite, object, object, object>>.Create(binder);

            callsite.Target(callsite, this, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_members.TryGetValue(binder.Name.ToLower(), out var value))
            {
                result = value;
                return true;
            }
            else
            {
                result = "Invalid Property!";
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _members[binder.Name.ToLower()] = value;
            return true;
        }
    }
}
