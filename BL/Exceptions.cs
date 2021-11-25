using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        #region in the main
        [Serializable]
        public class NegetiveValueException : Exception
        {
            public NegetiveValueException(string item)
                : base($"ERROR: {item} must be positive.") { }
            public NegetiveValueException(string item, int digits)
                : base($"ERROR: {item} must be positive, The {item} must be content {digits} digits.") { }
            public NegetiveValueException(string item, int digits, double v)
                : base($"ERROR: {item} must be positive, The {item} must be content at least {digits} digits (example:{v})") { }
        }

        [Serializable]
        public class OnlyDigitsException : Exception
        {
            public OnlyDigitsException(string message) : base($"ERROR: {message} must be only with digits.") { }
        }
        #endregion
        #region dal exception
        [Serializable]
        public class DalException : Exception
        {
            public DalException(Exception item) { }
        }
        #endregion

        public class ItemNotFoundException : Exception
        {
            public ItemNotFoundException(int item, string message) : base($"ERROR: {message} with id - {item} not exsist") { }
        }

        public class NoParcelException : Exception
        {
            public NoParcelException(string message1, string message2) : base($"ERROR: there are no parcel that are {message1} and not {message2} !") { }
        }

        public class OnlyAvailableDroneException : Exception
        {
            public OnlyAvailableDroneException(DroneStatuses status) : base($"ERROR: To connect drone to parcel the drone must be available, this drone is in {status}") { }
        }
        
        public class OnlyDeliveryDroneException : Exception
        {
            public OnlyDeliveryDroneException(DroneStatuses status) : base($"ERROR: To collect parcel by drone the drone must be in delivery, this drone is in {status}") { }
        }

        public class ParcelTooHeavyException : Exception
        {
            public ParcelTooHeavyException(WeightCategory weight) : base($"ERROR: This drone can carry a maximum of a {weight} parcel , all parcels are heavier") { }
        }

        public class NotEnoughBatteryException : Exception
        {
            public NotEnoughBatteryException(double battery) : base($"ERROR: This drone does not have enough battery to make a delivery, the charge level in this drone is {battery}.") { }
        }

        public class NotConnectException : Exception
        {
            public NotConnectException(int firstDroneID, int secondeDroneID, int parcelID)
                : base($"ERROR in ctor: This drone (ID - {firstDroneID}) does not belong to parcel with ID" +
                      $" - {parcelID}, this package belongs to drone with ID - {secondeDroneID}") { }
        }
        
    }
}
