using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class NegetiveValueException : Exception
    {
        public NegetiveValueException(string item)
            : base($"ERROR: {item} must be positive.") { }
        public NegetiveValueException(string item, int digits)
            : base($"ERROR: {item} must be positive, The {item} must be content {digits} digits.") { }
        public NegetiveValueException(string item, int digits, double v)
            : base($"ERROR: {item} must be positive, The {item} must be content at least {digits} digits (example:{v})") { }
    }

    public class OnlyDigitsException : Exception
    {
        public OnlyDigitsException(string message) : base($"ERROR: {message} must be only with digits.") { }
    }

    public class WrongEnumValuesException : Exception
    {
        public WrongEnumValuesException(string item, int first, int last) : base($"ERROR: {item} have only {first} - {last} values.") { }
    }

    public class DalException : Exception
    {
        public DalException(Exception item) : base(item.Message) { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int item, string message) : base($"ERROR: {message} with id - {item} not exsist") { }
    }

    public class NoParcelException : Exception
    {
        public NoParcelException(string message1, string message2) : base($"ERROR: there are no parcel that are {message1} and not {message2} !") { }
    }

    public class StatusDroneException : Exception
    {
        public StatusDroneException(string message, DroneStatuses actual, DroneStatuses mustBe) : base($"ERROR: To {message} the drone must be {mustBe}, this drone is in {actual}") { }
        public StatusDroneException(string message) : base($"ERROR: {message}") { }
    }

    public class ParcelTooHeavyException : Exception
    {
        public ParcelTooHeavyException(WeightCategory weight) : base($"ERROR: This drone can carry a maximum of a {weight} parcel , all parcels are heavier") { }
    }

    public class NotEnoughBatteryException : Exception
    {
        public NotEnoughBatteryException(string message, double battery) : base($"ERROR: This drone does not have enough battery to {message}, the charge level in this drone is {battery}.") { }
        public NotEnoughBatteryException(string message) : base($"ERROR: This drone does not have enough battery to {message}, The drone should be taken manually from the station !!!") { }
    }

    public class NotConnectException : Exception
    {
        public NotConnectException(int firstDroneID, int secondeDroneID, int parcelID)
            : base($"ERROR in ctor: This drone (ID - {firstDroneID}) does not belong to parcel with ID" +
                  $" - {parcelID}, this package belongs to drone with ID - {secondeDroneID}")
        { }
    }

    public class NoChargeSlotException : Exception
    {
        public NoChargeSlotException() : base($"ERROR: there are no stations with available charge slot") { }
    }

    public class SelfDeliveryException : Exception
    {
        public SelfDeliveryException() : base("ERROR: It's imposible to send parcel to yourself.\n") { }
    }

    public class EmptyListException : Exception
    {
        public EmptyListException(string message) : base($"ERROR: The list of {message} is empty") { }
    }

    public class EmptyInputException : Exception
    {
        public EmptyInputException(string message) : base($"ERROR: The {message} is empty") { }
    }

    public class IncorectInputException : Exception
    {
        public IncorectInputException(string message) : base($"ERROR: the {message} is incorrect") { }
        public IncorectInputException() : base($"ERROR: the input is incorrect") { }
    }
}
