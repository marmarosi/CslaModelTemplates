﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CslaModelTemplates.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CommonText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CslaModelTemplates.Resources.CommonText", typeof(CommonText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The application does not use roles..
        /// </summary>
        public static string AppPrincipal_NoRoles {
            get {
                return ResourceManager.GetString("AppPrincipal_NoRoles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The connection string of &apos;{0}&apos; database is missing..
        /// </summary>
        public static string DalFactory_DalManager_NoConnStr {
            get {
                return ResourceManager.GetString("DalFactory_DalManager_NoConnStr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The DAL manager type of &apos;{0}&apos; database is missing..
        /// </summary>
        public static string DalFactory_DalManager_NoDalMngr {
            get {
                return ResourceManager.GetString("DalFactory_DalManager_NoDalMngr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The database list is empty..
        /// </summary>
        public static string DalFactory_DalManager_NoDatabases {
            get {
                return ResourceManager.GetString("DalFactory_DalManager_NoDatabases", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; type has not been found..
        /// </summary>
        public static string DalFactory_DalManager_NotFound {
            get {
                return ResourceManager.GetString("DalFactory_DalManager_NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is invalid.DalManager type..
        /// </summary>
        public static string DalFactory_DalManager_WrongKey {
            get {
                return ResourceManager.GetString("DalFactory_DalManager_WrongKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} value must not be {1}..
        /// </summary>
        public static string NotZeroRule_MessageText {
            get {
                return ResourceManager.GetString("NotZeroRule_MessageText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided PasswordHasherCompatibilityMode is invalid..
        /// </summary>
        public static string PasswordJumbler_InvalidCompatibilityMode {
            get {
                return ResourceManager.GetString("PasswordJumbler_InvalidCompatibilityMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The iteration count must be a positive integer..
        /// </summary>
        public static string PasswordJumbler_InvalidIterationCount {
            get {
                return ResourceManager.GetString("PasswordJumbler_InvalidIterationCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ValidationResourceType attribute is missing from the business object..
        /// </summary>
        public static string Validation_MissingAttribute {
            get {
                return ResourceManager.GetString("Validation_MissingAttribute", resourceCulture);
            }
        }
    }
}
