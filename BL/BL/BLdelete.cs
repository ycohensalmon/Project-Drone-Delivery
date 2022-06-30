using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using BlApi;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int stationId)
        {
            try
            {
                lock (dalObj)
                {
                    dalObj.DeleteStation(stationId);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int droneId)
        {
            try
            {
                lock (dalObj)
                {
                    drones.Where(x => x.Id == droneId).First().IsDeleted = true;
                    dalObj.DeleteDrone(droneId);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int customerId)
        {
            try
            {
                lock (dalObj)
                {
                    dalObj.DeleteCustomer(customerId);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int parcelId)
        {
            try
            {
                lock (dalObj)
                {
                    dalObj.DeleteParcel(parcelId);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteUser(int userId)
        {
            try
            {
                lock (dalObj)
                {
                    dalObj.DeleteUser(userId);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }
    }
}
