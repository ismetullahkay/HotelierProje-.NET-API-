using HotelProject.DataAccessLayer.Abstract;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.DataAccessLayer.Repositories;
using HotelProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DataAccessLayer.EntityFramework
{
    public class EfRoomDal : GenericRepositoriy<Room>, IRoomDal
    {
        public EfRoomDal(Context context) : base(context) //GenericRepo da constructorunda contexi geçmiştik miras aldığımız için constructoru da mirasladık 
        {
        }
    }
}
