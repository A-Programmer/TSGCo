
using System.ComponentModel;
using AutoMapper;
using IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using KSFramework.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Auth.Areas.Admin.ViewModels;
using Project.Auth.Areas.Admin.ViewModels.Clients;
using Project.Auth.Data;
using Project.Auth.Extensions;
using Project.Auth.Services;
using static IdentityServer4.IdentityServerConstants;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin"), DisplayName("Manage Clients")]
    public class ClientsController : Controller
    {
        private readonly IClientServices _clients;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ClientsController(IClientServices clients, IMapper mapper, IUnitOfWork uow)
        {
            _clients = clients;
            _mapper = mapper;
            _uow = uow;
        }

        #region Clients
        #region List of clients
        [HttpGet, DisplayName("Clients List")]
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString)
        {
            var page = id ?? 1;
            var pageSize = 50;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var items = _clients.GetQueryable().AsQueryable();

            if(!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x =>
                    x.ClientName.ToLower().Contains(searchString) ||
                    x.ClientId.Contains(searchString) ||
                    x.Description.ToLower().Contains(searchString)
                );
            }

            var pagedItems = await PaginatedList<IdentityServer4.EntityFramework.Entities.Client>.CreateAsync(items, page, pageSize);

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if(isAjax)
                return PartialView("_ListPartialView", pagedItems);
            return View(pagedItems);
        }
        #endregion

        #region Client Details
        [HttpGet, DisplayName("Client Details")]
        public async Task<IActionResult> Details(int id)
        {
            var client = await _clients.GetById(id);

            ViewBag.ClientRedirectUris = new SelectList(await _clients.GetClientRedirectUrisAsync(id), "Id", "RedirectUri");
            ViewBag.PostLogoutRedirectUris = new SelectList(await _clients.GetPostLogoutRedirectUrisAsync(id), "Id", "RedirectPostLogoutUri");
            ViewBag.AllowedGrantTypes = new SelectList(await _clients.GetClientGrantTypesAsync(id), "Id", "GrantType");
            ViewBag.AllowedScopes = new SelectList(await _clients.GetClientScopesAsync(id), "Id", "Scope");
            ViewBag.AllowedCorsOrigins = new SelectList(await _clients.GetClientCorsOriginsAsync(id), "Id", "Origin");
            ViewBag.Properties = new SelectList(await _clients.GetClientPropertiesAsync(id), "Key", "Value");
            ViewBag.Claims = new SelectList(await _clients.GetClientClaimsAsync(id), "Type", "Value");
            ViewBag.IdentityProviderRestrictions = new SelectList(await _clients.GetClientIdPRestrictionsAsync(id), "Id", "Provider");
            
            return PartialView("_DetailsPartialView", client);
        }
        #endregion

        #region Add Client
        [HttpGet, DisplayName("Add Client")]
        public IActionResult Add()
        {
            return PartialView("_AddPartialView");
        }

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Adding Client"), ActionName("Add")]
        public async Task<IActionResult> AddPost(AddClientViewModel client)
        {

            var clientToAdd = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client>(client);

            var result = await _clients.AddClient(clientToAdd);

            TempData.Put("Message", result);
            return PartialView("_AddPartialView", client);
        }
        #endregion

        #region Update Client
        [HttpGet, DisplayName("Update Client")]
        public async Task<IActionResult> Update(int id)
        {
            var client = await _clients.GetById(id);
            var clientToEdit = _mapper.Map<EditClientViewModel>(client);
            return PartialView("_UpdatePartialView", clientToEdit);
        }

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Updateing Client"), ActionName("Update")]
        public async Task<IActionResult> UpdatePost(EditClientViewModel client)
        {
            var clientToEdit = _mapper.Map<IdentityServer4.EntityFramework.Entities.Client>(client);

            var result = await _clients.UpdateClient(clientToEdit);
            
            TempData.Put("Message", result);
            return PartialView("_UpdatePartialView", client);
        }
        #endregion
    

        #region Delete Client
        [HttpGet, DisplayName("Delete Client")]
        public IActionResult Delete(int id)
        {
            return PartialView("_DeletePartialView", id);
        }

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Deleting Client"), ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _clients.Delete(id);

            TempData.Put("Message", result);
            
            return PartialView("_DeletePartialView", id);
        }
        #endregion

        #endregion



        #region Secrets
        #region Secrets List
        [HttpGet, DisplayName("Client Secrets List")]
        public async Task<IActionResult> Secrets(int id)
        {
            ViewData["ClientId"] = id;

            var secrets = await _clients.GetClientSecretsAsync(id);
            
            return PartialView("_SecretsPartialView", secrets);
        }
        #endregion

        #region Remove All Client Secrets
        [HttpGet, DisplayName("Remove All Client Secrets")]
        public IActionResult RemoveAllClientSecrets(int id)
        {
            return PartialView("_RemoveAllClientSecretsPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client Secrets"), ActionName("RemoveAllClientSecrets")]
        public async Task<IActionResult> RemoveAllClientSecretsPost(int id)
        {
            var result = await _clients.RemoveAllClientSecrets(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientSecretsPartialView", id);
        }
        #endregion

        #region  AddSecret
        [HttpGet, DisplayName("Add Client Secret")]
        public IActionResult AddSecret(int id)
        {
            var clientSecret = new AddClientSecretViewModel() { ClientId = id };
            var types = new Dictionary<string, string>();
            types.Add(SecretTypes.JsonWebKey, "JWK");
            types.Add(SecretTypes.SharedSecret, "SharedSecret");
            types.Add(SecretTypes.X509CertificateThumbprint, "X509Thumbprint");
            types.Add(SecretTypes.X509CertificateName, "X509Name");
            types.Add(SecretTypes.X509CertificateBase64, "X509CertificateBase64");
            ViewBag.Types = new SelectList(types, "Value", "Key");
            return PartialView("_AddSecretPartialView", clientSecret);
        }
        [HttpPost, ActionName("AddSecret"), DisplayName("Adding Client Secret")]
        public async Task<IActionResult> AddSecretPost(AddClientSecretViewModel clientSecretVm)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddSecretPartialView", clientSecretVm);
            }
            var clientSecret = new ClientSecret()
            {
                ClientId = clientSecretVm.ClientId,
                Type = clientSecretVm.Type,
                Value = clientSecretVm.Value.Sha256(),
                Description = clientSecretVm.Description,
                Expiration = clientSecretVm.Expiration,
                Created = DateTime.Now
            };
            var result = await _clients.AddClientSecret(clientSecret);
            TempData.Put("Message", result);
            
            return PartialView("_AddSecretPartialView", clientSecretVm);
        }
        #endregion
        
        #region Delete Client Secret

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client Secret")]
        public async Task<IActionResult> DeleteSecret(int id)
        {
            var result = await _clients.DeleteSecret(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion

        
        #endregion

        #region Client Redirect Uris
        #region Redirect Uris List
        [HttpGet, DisplayName("Client Redirect Uris List")]
        public async Task<IActionResult> RedirectUris(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientRedirectUrisAsync(id);
            
            return PartialView("_RedirectUrisPartialView", entities);
        }
        #endregion

        #region Remove All Client Redirect Uris
        [HttpGet, DisplayName("Remove All Client Redirect Uris")]
        public IActionResult RemoveAllClientRedirectUris(int id)
        {
            return PartialView("_RemoveAllClientRedirectUrisPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client Redirect Uris"), ActionName("RemoveAllClientRedirectUris")]
        public async Task<IActionResult> RemoveAllClientRedirectUrisPost(int id)
        {
            var result = await _clients.RemoveAllClientSecrets(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientRedirectUrisPartialView", id);
        }
        #endregion

        #region  Add Client Redirect Uri
        [HttpGet, DisplayName("Add Client Redirect Uri")]
        public IActionResult AddRedirectUri(int id)
        {
            var entity = new AddRedirectUriViewModel() { ClientId = id };
            return PartialView("_AddRedirectUriPartialView", entity);
        }
        [HttpPost, ActionName("AddRedirectUri"), DisplayName("Adding Client Redirect Uri")]
        public async Task<IActionResult> AddRedirectUriPost(AddRedirectUriViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddRedirectUriPartialView", viewModel);
            }
            var entity = new ClientRedirectUri()
            {
                ClientId = viewModel.ClientId,
                RedirectUri = viewModel.RedirectUri
            };
            var result = await _clients.AddRedirectUri(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddRedirectUriPartialView", viewModel);
        }
        #endregion

        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client Redirect Uri")]
        public async Task<IActionResult> DeleteClientRedirectUri(int id)
        {
            var result = await _clients.DeleteClientRedirectUri(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region Client Post Logout Redirect Uris
        #region Post Logout Redirect Uris
        [HttpGet, DisplayName("Client Post Logout Redirect Uris List")]
        public async Task<IActionResult> PostLogoutRedirectUris(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetPostLogoutRedirectUrisAsync(id);
            
            return PartialView("_PostLogoutRedirectUrisPartialView", entities);
        }
        #endregion

        #region Remove All Client Post Logout Redirect Uris
        [HttpGet, DisplayName("Remove All Client Post Logout Redirect Uris")]
        public IActionResult RemoveAllClientPostLogoutRedirectUris(int id)
        {
            return PartialView("_RemoveAllClientPostLogoutRedirectUrisPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client Post Logout Redirect Uris"), ActionName("RemoveAllClientPostLogoutRedirectUris")]
        public async Task<IActionResult> RemoveAllClientPostLogoutRedirectUrisPost(int id)
        {
            var result = await _clients.RemoveAllPostLogoutRedirectUris(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientPostLogoutRedirectUrisPartialView", id);
        }
        #endregion

        #region  Add Client Post Logout Redirect Uri
        [HttpGet, DisplayName("Add Client Post Logout Redirect Uri")]
        public IActionResult AddPostLogoutRedirectUri(int id)
        {
            var entity = new AddPostLogoutRedirectUriViewModel() { ClientId = id };
            return PartialView("_AddPostLogoutRedirectUriPartialView", entity);
        }
        [HttpPost, ActionName("AddPostLogoutRedirectUri"), DisplayName("Adding Client Post Logout Redirect Uri")]
        public async Task<IActionResult> AddPostLogoutRedirectUriPost(AddPostLogoutRedirectUriViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddPostLogoutRedirectUriPartialView", viewModel);
            }
            var entity = new ClientPostLogoutRedirectUri()
            {
                ClientId = viewModel.ClientId,
                PostLogoutRedirectUri = viewModel.PostLogoutRedirectUri
            };
            var result = await _clients.AddPostLogoutRedirectUri(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddPostLogoutRedirectUriPartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client Post Logout Redirect Uri")]
        public async Task<IActionResult> DeleteClientPostLogoutRedirectUri(int id)
        {
            var result = await _clients.DeleteClientPostLogoutRedirectUri(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion


        #region Client Grant Types
        #region Grant Types
        [HttpGet, DisplayName("Client Grant Types List")]
        public async Task<IActionResult> GrantTypes(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientGrantTypesAsync(id);
            
            return PartialView("_GrantTypesPartialView", entities);
        }
        #endregion

        #region Remove All Client GrantTypes
        [HttpGet, DisplayName("Remove All Client GrantTypes")]
        public IActionResult RemoveAllClientGrantTypes(int id)
        {
            return PartialView("_RemoveAllClientGrantTypesPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client GrantTypes"), ActionName("RemoveAllClientGrantTypes")]
        public async Task<IActionResult> RemoveAllClientGrantTypesPost(int id)
        {
            var result = await _clients.RemoveAllClientGrantTypes(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientGrantTypesPartialView", id);
        }
        #endregion

        #region  Add Client GrantType
        [HttpGet, DisplayName("Add Client GrantType")]
        public IActionResult AddGrantType(int id)
        {
            var entity = new AddGrantTypeViewModel() { ClientId = id };
            return PartialView("_AddGrantTypePartialView", entity);
        }
        [HttpPost, ActionName("AddGrantType"), DisplayName("Adding Client GrantType")]
        public async Task<IActionResult> AddGrantTypePost(AddGrantTypeViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddGrantTypePartialView", viewModel);
            }
            var entity = new ClientGrantType()
            {
                ClientId = viewModel.ClientId,
                GrantType = viewModel.GrantType
            };
            var result = await _clients.AddClientGrantType(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddGrantTypePartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client GrantType")]
        public async Task<IActionResult> DeleteClientGrantType(int id)
        {
            var result = await _clients.DeleteClientGrantType(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion


        #region Client Scopes
        #region Scopes
        [HttpGet, DisplayName("Client Allowed Scopes List")]
        public async Task<IActionResult> Scopes(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientScopesAsync(id);
            
            return PartialView("_ScopesPartialView", entities);
        }
        #endregion

        #region Remove All Client Scopes
        [HttpGet, DisplayName("Remove All Client Scopes")]
        public IActionResult RemoveAllClientScopes(int id)
        {
            return PartialView("_RemoveAllClientScopesPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client Scopes"), ActionName("RemoveAllClientScopes")]
        public async Task<IActionResult> RemoveAllClientScopesPost(int id)
        {
            var result = await _clients.RemoveAllClientScopes(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientScopesPartialView", id);
        }
        #endregion

        #region  Add Client Scope
        [HttpGet, DisplayName("Add Client Scope")]
        public IActionResult AddScope(int id)
        {
            var entity = new AddScopeViewModel() { ClientId = id };
            return PartialView("_AddScopePartialView", entity);
        }
        [HttpPost, ActionName("AddScope"), DisplayName("Adding Client Scope")]
        public async Task<IActionResult> AddScopePost(AddScopeViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddScopePartialView", viewModel);
            }
            var entity = new ClientScope()
            {
                ClientId = viewModel.ClientId,
                Scope = viewModel.Scope
            };
            var result = await _clients.AddClientScope(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddScopePartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client Scope")]
        public async Task<IActionResult> DeleteClientScope(int id)
        {
            var result = await _clients.DeleteClientScope(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region Client Cors Origins
        #region Cors Origins
        [HttpGet, DisplayName("Client Cors Origins List")]
        public async Task<IActionResult> CorsOrigins(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientCorsOriginsAsync(id);
            
            return PartialView("_CorsOriginsPartialView", entities);
        }
        #endregion

        #region Remove All Client CorsOrigins
        [HttpGet, DisplayName("Remove All Client CorsOrigins")]
        public IActionResult RemoveAllClientCorsOrigins(int id)
        {
            return PartialView("_RemoveAllClientCorsOriginsPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client CorsOrigins"), ActionName("RemoveAllClientCorsOrigins")]
        public async Task<IActionResult> RemoveAllClientCorsOriginsPost(int id)
        {
            var result = await _clients.RemoveAllClientCorsOrigins(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientCorsOriginsPartialView", id);
        }
        #endregion

        #region  Add Client CorsOrigin
        [HttpGet, DisplayName("Add Client CorsOrigin")]
        public IActionResult AddCorsOrigin(int id)
        {
            var entity = new AddCorsOriginViewModel() { ClientId = id };
            return PartialView("_AddCorsOriginPartialView", entity);
        }
        [HttpPost, ActionName("AddCorsOrigin"), DisplayName("Adding Client CorsOrigin")]
        public async Task<IActionResult> AddCorsOriginPost(AddCorsOriginViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddCorsOriginPartialView", viewModel);
            }
            var entity = new ClientCorsOrigin()
            {
                ClientId = viewModel.ClientId,
                Origin = viewModel.Origin
            };
            var result = await _clients.AddCorsOrigin(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddCorsOriginPartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client CorsOrigin")]
        public async Task<IActionResult> DeleteClientCorsOrigin(int id)
        {
            var result = await _clients.DeleteClientCorsOrigin(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region Client Properties
        #region Properties
        [HttpGet, DisplayName("Client Properties List")]
        public async Task<IActionResult> Properties(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientPropertiesAsync(id);
            
            return PartialView("_PropertiesPartialView", entities);
        }
        #endregion

        #region Remove All Client Properties
        [HttpGet, DisplayName("Remove All Client Properties")]
        public IActionResult RemoveAllClientProperties(int id)
        {
            return PartialView("_RemoveAllClientPropertiesPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client Properties"), ActionName("RemoveAllClientProperties")]
        public async Task<IActionResult> RemoveAllClientPropertiesPost(int id)
        {
            var result = await _clients.RemoveAllClientProperties(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientPropertiesPartialView", id);
        }
        #endregion

        #region  Add Client Property
        [HttpGet, DisplayName("Add Client Property")]
        public IActionResult AddProperty(int id)
        {
            var entity = new AddPropertyViewModel() { ClientId = id };
            return PartialView("_AddPropertyPartialView", entity);
        }
        [HttpPost, ActionName("AddProperty"), DisplayName("Adding Client Property")]
        public async Task<IActionResult> AddPropertyPost(AddPropertyViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddPropertyPartialView", viewModel);
            }
            var entity = new ClientProperty()
            {
                ClientId = viewModel.ClientId,
                Key = viewModel.Key,
                Value = viewModel.Value
            };
            var result = await _clients.AddClientProperty(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddPropertyPartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client Property")]
        public async Task<IActionResult> DeleteClientProperty(int id)
        {
            var result = await _clients.DeleteClientProperty(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region Client Claims
        #region Claims
        [HttpGet, DisplayName("Client Claims List")]
        public async Task<IActionResult> Claims(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientClaimsAsync(id);
            
            return PartialView("_ClaimsPartialView", entities);
        }
        #endregion

        #region Remove All Client Claims
        [HttpGet, DisplayName("Remove All Client Claims")]
        public IActionResult RemoveAllClientClaims(int id)
        {
            return PartialView("_RemoveAllClientClaimsPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client Claims"), ActionName("RemoveAllClientClaims")]
        public async Task<IActionResult> RemoveAllClientClaimsPost(int id)
        {
            var result = await _clients.RemoveAllClientClaims(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientClaimsPartialView", id);
        }
        #endregion

        #region  Add Client Claim
        [HttpGet, DisplayName("Add Client Claim")]
        public IActionResult AddClaim(int id)
        {
            var entity = new AddClaimViewModel() { ClientId = id };
            return PartialView("_AddClaimPartialView", entity);
        }
        [HttpPost, ActionName("AddClaim"), DisplayName("Adding Client Claim")]
        public async Task<IActionResult> AddClaimPost(AddClaimViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddClaimPartialView", viewModel);
            }
            var entity = new IdentityServer4.EntityFramework.Entities.ClientClaim()
            {
                ClientId = viewModel.ClientId,
                Type = viewModel.Type,
                Value = viewModel.Value
            };
            var result = await _clients.AddClientClaim(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddClaimPartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client Claim")]
        public async Task<IActionResult> DeleteClientClaim(int id)
        {
            var result = await _clients.DeleteClientClaim(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region Client Identity Provider Restrictions
        #region Identity Provider Restrictions
        [HttpGet, DisplayName("Client Identity Provider Restrictions List")]
        public async Task<IActionResult> IdentityProviderRestrictions(int id)
        {
            ViewData["ClientId"] = id;

            var entities = await _clients.GetClientIdPRestrictionsAsync(id);
            
            return PartialView("_IdentityProviderRestrictionsPartialView", entities);
        }
        #endregion

        #region Remove All Client IdentityProviderRestrictions
        [HttpGet, DisplayName("Remove All Client IdentityProviderRestrictions")]
        public IActionResult RemoveAllClientIdentityProviderRestrictions(int id)
        {
            return PartialView("_RemoveAllClientIdentityProviderRestrictionsPartialView", id);
        }
        [HttpPost, DisplayName("Removing All Client IdentityProviderRestrictions"), ActionName("RemoveAllClientIdentityProviderRestrictions")]
        public async Task<IActionResult> RemoveAllClientIdentityProviderRestrictionsPost(int id)
        {
            var result = await _clients.RemoveAllClientIdPRestrictions(id);

            TempData.Put("Message", result);
            return PartialView("_RemoveAllClientIdentityProviderRestrictionsPartialView", id);
        }
        #endregion

        #region  Add Client IdentityProviderRestriction
        [HttpGet, DisplayName("Add Client IdentityProviderRestriction")]
        public IActionResult AddIdentityProviderRestriction(int id)
        {
            var entity = new AddIdentityProviderRestrictionViewModel() { ClientId = id };
            return PartialView("_AddIdentityProviderRestrictionPartialView", entity);
        }
        [HttpPost, ActionName("AddIdentityProviderRestriction"), DisplayName("Adding Client IdentityProviderRestriction")]
        public async Task<IActionResult> AddIdentityProviderRestrictionPost(AddIdentityProviderRestrictionViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddIdentityProviderRestrictionPartialView", viewModel);
            }
            var entity = new ClientIdPRestriction()
            {
                ClientId = viewModel.ClientId,
                Provider = viewModel.Provider
            };
            var result = await _clients.AddClientIdPRestriction(entity);
            
            TempData.Put("Message", result);
            
            return PartialView("_AddIdentityProviderRestrictionPartialView", viewModel);
        }
        #endregion
        #region Delete

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Delete Client IdentityProviderRestriction")]
        public async Task<IActionResult> DeleteClientIdPRestriction(int id)
        {
            var result = await _clients.DeleteClientIdPRestriction(id);

            TempData.Put("Message", result);
            
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

    }
}