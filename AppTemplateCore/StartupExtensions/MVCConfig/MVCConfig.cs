using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class MVCConfig
    {

        public static void Add_MVC_Config(this IServiceCollection services)
        {
            // Add Services & Configure their Settings needed by MVC Middleware
            services.AddMvc(options =>
            {
                // Authorization Policy for All Controllers of Application
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                // Auth Policy + Auth Filter to MVC Framework
                // All Controller Actions now need Logged In User.
                // Anonymouse Users can get access to Controll Action having 
                // Allow Anonymous Attribute.
                options.Filters.Add(new AuthorizeFilter(policy));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }


        public static void Add_MVC_Config2(this IServiceCollection services)
        {

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });




            // Add Services & Configure their Settings needed by MVC Middleware
            services.AddMvc(options =>
            {
                // Authorization Policy for All Controllers of Application
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                // Auth Policy + Auth Filter to MVC Framework
                // All Controller Actions now need Logged In User.
                // Anonymouse Users can get access to Controll Action having 
                // Allow Anonymous Attribute.
                options.Filters.Add(new AuthorizeFilter(policy));


                // Summary:
                //     Gets or sets a value that determines if HeaderModelBinder
                //     should bind to types other than System.String or a collection of System.String.
                //     If set to true, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.HeaderModelBinder
                //     would bind to simple types (like System.String, System.Int32, System.Enum, System.Boolean
                //     etc.) or a collection of simple types. The default value of the property is false.
                options.AllowBindingHeaderValuesToNonStringModelTypes = true;

                // Summary:
                //     Gets or sets a value that determines if policies on instances of Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter
                //     will be combined into a single effective policy. The default value of the property
                //     is false.
                //
                // Remarks:
                //     Authorization policies are designed such that multiple authorization policies
                //     applied to an endpoint should be combined and executed a single policy. The Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter
                //     (commonly applied by Microsoft.AspNetCore.Authorization.AuthorizeAttribute) can
                //     be applied globally, to controllers, and to actions - which specifies multiple
                //     authorization policies for an action. In all ASP.NET Core releases prior to 2.1
                //     these multiple policies would not combine as intended. This compatibility switch
                //     configures whether the old (unintended) behavior or the new combining behavior
                //     will be used when multiple authorization policies are applied.
                //     This property is associated with a compatibility switch and can provide a different
                //     behavior depending on the configured compatibility version for the application.
                //     See Microsoft.AspNetCore.Mvc.CompatibilityVersion for guidance and examples of
                //     setting the application's compatibility version.
                //     Configuring the desired value of the compatibility switch by calling this property's
                //     setter will take precedence over the value implied by the application's Microsoft.AspNetCore.Mvc.CompatibilityVersion.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_0
                //     then this setting will have the value false unless explicitly configured.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1
                //     or higher then this setting will have the value true unless explicitly configured.
                options.AllowCombiningAuthorizeFilters = true;

                // Summary:
                //     Gets or sets the flag which decides whether body model binding (for example,
                //     on an action method parameter with Microsoft.AspNetCore.Mvc.FromBodyAttribute)
                //     should treat empty input as valid. false by default.
                options.AllowEmptyInputInBodyModelBinding = true;

                // Summary:
                //     Gets or sets a value that determines if Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidationVisitor
                //     can short-circuit validation when a model does not have any associated validators.
                //
                // Remarks:
                //     When Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.HasValidators is true,
                //     that is, it is determined that a model or any of it's properties or collection
                //     elements cannot have any validators, Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidationVisitor
                //     can short-circuit validation for the model and mark the object graph as valid.
                //     Setting this property to true, allows Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidationVisitor
                //     to perform this optimization.
                //     This property is associated with a compatibility switch and can provide a different
                //     behavior depending on the configured compatibility version for the application.
                //     See Microsoft.AspNetCore.Mvc.CompatibilityVersion for guidance and examples of
                //     setting the application's compatibility version.
                //     Configuring the desired value of the compatibility switch by calling this property's
                //     setter will take precedence over the value implied by the application's Microsoft.AspNetCore.Mvc.CompatibilityVersion.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2
                //     then this setting will have the value true unless explicitly configured.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1
                //     or earlier then this setting will have the value false unless explicitly configured.
                options.AllowShortCircuitingValidationWhenNoValidatorsArePresent = true;

                // Summary:
                //     Gets or sets a value that determines if model bound action parameters, controller
                //     properties, page handler parameters, or page model properties are validated (in
                //     addition to validating their elements or properties). If set to true, Microsoft.AspNetCore.Mvc.ModelBinding.BindRequiredAttribute
                //     and ValidationAttributes on these top-level nodes are checked. Otherwise, such
                //     attributes are ignored.
                //
                // Remarks:
                //     This property is associated with a compatibility switch and can provide a different
                //     behavior depending on the configured compatibility version for the application.
                //     See Microsoft.AspNetCore.Mvc.CompatibilityVersion for guidance and examples of
                //     setting the application's compatibility version.
                //     Configuring the desired value of the compatibility switch by calling this property's
                //     setter will take precedence over the value implied by the application's Microsoft.AspNetCore.Mvc.CompatibilityVersion.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_0
                //     then this setting will have the value false unless explicitly configured.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1
                //     or higher then this setting will have the value true unless explicitly configured.
                options.AllowValidatingTopLevelNodes = true;

                // Summary:
                // Gets a Dictionary of CacheProfile Names, CacheProfile
                // which are pre-defined settings for response caching.
                //options.CacheProfiles.Add();

                // Summary:
                // Gets a list of ApplicationModels.IApplicationModelConvention
                // instances that will be applied to the ApplicationModels.ApplicationModel
                // when discovering actions.
                //options.Conventions.Add()

                // Summary:
                // Gets or sets a value that determines if routing should use endpoints internally,
                // or if legacy routing logic should be used. Endpoint routing is used to match
                // HTTP requests to MVC actions, and to generate URLs with IUrlHelper.
                options.EnableEndpointRouting = true;

                // Summary:
                // Gets a collection of Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata which are
                // used to construct filters that apply to all actions.
                //options.Filters.Add();

                // Summary:
                // Used to specify mapping between the URL Format and corresponding media type.
                //options.FormatterMappings

                // Summary:
                // Gets or sets the maximum number of validation errors that are allowed by this
                // application before further errors are ignored.
                options.MaxModelValidationErrors = 5;


                // Summary:
                //     Gets or sets the maximum depth to constrain the validation visitor when validating.
                //     Set to null to disable this feature.
                //     Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidationVisitor traverses
                //     the object graph of the model being validated. For models that are very deep
                //     or are infinitely recursive, validation may result in stack overflow.
                //     When not null, Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidationVisitor
                //     will throw if traversing an object exceeds the maximum allowed validation depth.
                //     This property is associated with a compatibility switch and can provide a different
                //     behavior depending on the configured compatibility version for the application.
                //     See Microsoft.AspNetCore.Mvc.CompatibilityVersion for guidance and examples of
                //     setting the application's compatibility version.
                //     Configuring the desired value of the compatibility switch by calling this property's
                //     setter will take precedence over the value implied by the application's Microsoft.AspNetCore.Mvc.CompatibilityVersion.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2
                //     then this setting will have the value 200 unless explicitly configured.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1
                //     or earlier then this setting will have the value null unless explicitly configured.
                //options.MaxValidationDepth = null;

                // Summary:
                //     Gets a list of Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinderProviders used
                //     by this application.
                //options.ModelBinderProviders.Add();

                // Summary:
                //     Gets the default Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.ModelBindingMessageProvider.
                //     Changes here are copied to the Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.ModelBindingMessageProvider
                //     property of all Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata instances
                //     unless overridden in a custom Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.IBindingMetadataProvider.
                //options.ModelBindingMessageProvider

                // Summary:
                //     Gets a list of Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.IMetadataDetailsProvider
                //     instances that will be used to create Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata
                //     instances.
                //
                // Remarks:
                //     A provider should implement one or more of the following interfaces, depending
                //     on what kind of details are provided: Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.IBindingMetadataProvider
                //     Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.IDisplayMetadataProvider Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.IValidationMetadataProvider
                //options.ModelMetadataDetailsProviders.Add();


                // Summary:
                //     Gets a list of Microsoft.AspNetCore.Mvc.Formatters.IOutputFormatters that are
                //     used by this application.
                //options.OutputFormatters.Add();

                // Summary:
                //     Gets or sets the default value for the Permanent property of Microsoft.AspNetCore.Mvc.RequireHttpsAttribute.
                //options.RequireHttpsPermanent = true;

                // Summary:
                // Gets or sets the flag which causes content negotiation to ignore Accept header
                // when it contains the media type */*. false by default.
                //options.RespectBrowserAcceptHeader = true;

                // Summary:
                //     Gets or sets the flag which decides whether an HTTP 406 Not Acceptable response
                //     will be returned if no formatter has been selected to format the response. false
                //     by default.
                //options.ReturnHttpNotAcceptable = true;

                // Summary:
                //     Gets or sets the SSL port that is used by this application when Microsoft.AspNetCore.Mvc.RequireHttpsAttribute
                //     is used. If not set the port won't be specified in the secured URL e.g. https://localhost/path.
                //options.SslPort = null;


                // Summary:
                //     Gets or sets a value indicating whether the model binding system will bind undefined
                //     values to enum types. The default value of the property is false.
                //
                // Remarks:
                //     This property is associated with a compatibility switch and can provide a different
                //     behavior depending on the configured compatibility version for the application.
                //     See Microsoft.AspNetCore.Mvc.CompatibilityVersion for guidance and examples of
                //     setting the application's compatibility version.
                //     Configuring the desired value of the compatibility switch by calling this property's
                //     setter will take precedence over the value implied by the application's Microsoft.AspNetCore.Mvc.CompatibilityVersion.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_0
                //     then this setting will have the value false unless explicitly configured.
                //     If the application's compatibility version is set to Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1
                //     or higher then this setting will have the value true unless explicitly configured.
                //options.SuppressBindingUndefinedValueToEnumType = null; 


                // Summary:
                //     Gets or sets the flag to buffer the request body in input formatters. Default
                //     is false.
                //options.SuppressInputFormatterBuffering 

                // Summary:
                //     Gets a list of Microsoft.AspNetCore.Mvc.ModelBinding.IValueProviderFactory used
                //     by this application.
                //options.ValueProviderFactories.Add();


            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }


        public static void Add_MVC_Config3(this IServiceCollection services)
        {

            services.Configure<MvcOptions>(options =>
            {
                // Authorization Policy for All Controllers of Application
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                // Auth Policy + Auth Filter to MVC Framework
                // All Controller Actions now need Logged In User.
                // Anonymouse Users can get access to Controll Action having 
                // Allow Anonymous Attribute.
                options.Filters.Add(new AuthorizeFilter(policy));
            });


        }




    }
}
