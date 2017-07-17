
1. add-migration 01 -ConfigurationTypeName ClientConfiguration 
   update-database -ConfigurationTypeName ClientConfiguration -verbose

2. add-migration 01 -ConfigurationTypeName OperationalConfiguration 
   update-database -ConfigurationTypeName OperationalConfiguration -verbose
   
3. add-migration 01 -ConfigurationTypeName ScopeConfiguration 
   update-database -ConfigurationTypeName ScopeConfiguration -verbose


4. https://github.com/IdentityServer/IdentityServer3.EntityFramework/issues/116


5. add-migration 01 -ConfigurationTypeName ModelsContainerConfiguration 
   update-database -ConfigurationTypeName ModelsContainerConfiguration -verbose
