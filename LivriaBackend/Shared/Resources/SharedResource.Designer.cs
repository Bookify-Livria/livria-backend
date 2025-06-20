
namespace LivriaBackend.Shared.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SharedResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SharedResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LivriaBackend.Shared.Resources.SharedResource", typeof(SharedResource).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date not in range: {0} [{1}-Today]..
        /// </summary>
        internal static string DateNotInRange {
            get {
                return ResourceManager.GetString("DateNotInRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date out of range: {0} [{1}-{2}]..
        /// </summary>
        internal static string DateOutOfRange {
            get {
                return ResourceManager.GetString("DateOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email error: {0}..
        /// </summary>
        internal static string EmailError {
            get {
                return ResourceManager.GetString("EmailError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field is required: {0}..
        /// </summary>
        internal static string EmptyField {
            get {
                return ResourceManager.GetString("EmptyField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Length error: {0} [{2}-{1}]..
        /// </summary>
        internal static string LengthError {
            get {
                return ResourceManager.GetString("LengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Max length error: {0} [{1}]..
        /// </summary>
        internal static string MaxLengthError {
            get {
                return ResourceManager.GetString("MaxLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be a phone number: {0}..
        /// </summary>
        internal static string PhoneError {
            get {
                return ResourceManager.GetString("PhoneError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be in range: {0} [{1}-{2}]..
        /// </summary>
        internal static string RangeError {
            get {
                return ResourceManager.GetString("RangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be an URL: {0}..
        /// </summary>
        internal static string UrlError {
            get {
                return ResourceManager.GetString("UrlError", resourceCulture);
            }
        }
    }
}
