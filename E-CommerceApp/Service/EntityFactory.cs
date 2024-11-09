using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;

namespace E_CommerceApp.Service
{
    public class EntityFactory 
    {


        public IEntityService GetEntityService(Type entitTypey) 
        {
            if (entitTypey == typeof(Product))
                return new ProductService();

            else throw new NotImplementedException();
        }
    }
}
