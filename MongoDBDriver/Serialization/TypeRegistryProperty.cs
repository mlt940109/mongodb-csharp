﻿using System;
using System.Reflection;

namespace MongoDB.Driver.Serialization
{
    public class TypeRegistryProperty
    {
        public delegate object GetValueFunc(object instance);
        public delegate void SetValueAction(object instance, object value);

        private readonly GetValueFunc _getValue;
        private readonly SetValueAction _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeRegistryProperty"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <param name="propertyInfo">The property info.</param>
        public TypeRegistryProperty(string name, Type ownerType, PropertyInfo propertyInfo ){
            if(name == null)
                throw new ArgumentNullException("name");
            if(ownerType == null)
                throw new ArgumentNullException("ownerType");
            if(propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            MongoName = name;
            OwnerType = ownerType;
            //Todo: replace with reflection emit one
            _getValue = i=>propertyInfo.GetValue(i,null);
            //Todo: replace with reflection emit one
            _setValue = (i, o) => propertyInfo.SetValue(i, o, null);
        }

        /// <summary>
        /// Gets or sets the name of the mongo.
        /// </summary>
        /// <value>The name of the mongo.</value>
        public string MongoName { get; private set; }

        /// <summary>
        /// Gets or sets the type of the owner.
        /// </summary>
        /// <value>The type of the owner.</value>
        public Type OwnerType { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public object GetValue(object instance){
            return _getValue(instance);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="value">The value.</param>
        public void SetValue(object instance, object value){
            _setValue(instance, value);
        }
    }
}