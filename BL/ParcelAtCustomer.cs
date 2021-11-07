using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelAtCustomer
        {
            private Int32 id;
            private DateTime requested;
            private DateTime scheduled;
            private DateTime pickedUp;
            private DateTime delivered;
            private WeightCategory weight;
            private Priority priorities;
            private CustomerInParcel customerInParcel;

            public Int32 Id { get => id; set => id = value; }
            public DateTime Requested { get => requested; set => requested = value; }
            public DateTime Scheduled { get => scheduled; set => scheduled = value; }
            public DateTime PickedUp { get => pickedUp; set => pickedUp = value; }
            public DateTime Delivered { get => delivered; set => delivered = value; }
            public WeightCategory Weight { get => weight; set => weight = value; }
            public Priority Priorities { get => priorities; set => priorities = value; }
            public CustomerInParcel CustomerInParcel { get => customerInParcel; set => customerInParcel = value; }
        }
    }
}
