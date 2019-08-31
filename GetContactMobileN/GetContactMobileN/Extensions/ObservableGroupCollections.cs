using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GetContactMobileN.Extensions
{
    public class ObservableGroupCollections<S,T> : ObservableCollection<T>
    {
        private readonly S _key;
        public ObservableGroupCollections(IGrouping<S,T> group):base(group)
        {
            _key = group.Key;
        }

        public S Key
        {
            get
            {
                return _key;
            }
        }
    }
}
