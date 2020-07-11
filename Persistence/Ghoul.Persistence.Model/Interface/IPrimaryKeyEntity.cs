using System;

namespace Ghoul.Persistence.Model.Interface {

    public interface IPrimaryKeyEntity {
        Guid ID { get; set; }
    }

}