using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore.Domain.Entities
{
    public class EntityStr : EntityBase<string>
    {

        public EntityStr()
        {
            base.Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
