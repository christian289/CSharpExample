using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageDownload
{
    public enum State
    {
        NORMAL,
        HOLD,
        UNLIST
    }

    public enum Platform
    {
        ELEVEN_STREET,
        GMARKET,
        NONE
    }

    public enum Type
    {
        THUMBNAIL,
        BODY
    }

    public class EntityCategoryComparer : IEqualityComparer<Entity._Category>
    {
        public bool Equals(Entity._Category x, Entity._Category y)
        {
            return x.CategoryId == y.CategoryId && x.CategoryName == y.CategoryName ? true : false;
        }

        public int GetHashCode([DisallowNull] Entity._Category obj)
        {
            return obj.CategoryId.GetHashCode() ^ obj.CategoryName.GetHashCode();
        }
    }

    public class Entity
    {
        public string Name { get; set; }
        public DateTime HashUpdatedAt { get; set; }
        public Platform Platform { get; set; }
        public State State { get; set; }
        public _Category Category { get; set; }

        public List<_Image> Images { get; set; }

        public class _Category
        {
            public string CategoryId { get; set; }
            public string CategoryName { get; set; }
        }

        public class _Image
        {
            public decimal? Height { get; set; }
            public decimal? Witdh { get; set; }
            public Type Type { get; set; }
            public Uri Url { get; set; }
        }
    }
}
