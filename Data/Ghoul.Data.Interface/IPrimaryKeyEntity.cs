using System;

namespace Ghoul.Data.Interface {

    public interface IPrimaryKeyEntity {
        Guid ID { get; set; }
    }

}