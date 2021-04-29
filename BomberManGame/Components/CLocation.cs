using System;
namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Location Component. Responsible for storying a reference to the
    /// cell an Entity is currently located in. Most/All entities need this.
    /// </summary>
    public class CLocation: Component
    {
        /// <summary>
        /// Constructor. Initialises Location property to the passed in cell.
        /// </summary>
        /// <param name="self">The Entity this component belongs too.</param>
        /// <param name="location">The cell this entity is located in.</param>
        internal CLocation(Entity self, Cell location): base (self) => Location = location;

        /// <summary>
        /// Property for to access the reference to a cell representing the entitie's
        /// location.
        /// </summary>
        public Cell Location { get; set; }
    }
}
