//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using Dwp.Adep.Ucb.ResourceLibrary;
using AutoMapper;
using UcbWeb.Helpers;
using UcbWeb.ViewModels;
using UcbWeb.Models;
using UcbWeb.UcbService;


namespace UcbWeb.Controllers
{
    public partial class CustomerController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public CustomerController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public CustomerController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /Customer/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.CustomerCode;
			
            CustomerVM model = new CustomerVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.CustomerItem = new CustomerModel();
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get Customer item and any associated lookups    
                    CustomerVMDC returnedObject = sc.GetCustomer(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertCustomerDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.CustomerServiceVersion = model.CustomerItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved Customer to session
            sessionManager.CurrentCustomer = model.CustomerItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /Customer/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateCustomer(FormCollection collection)
        {
            return UpdateCustomer();
        }

        // POST: /Customer/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveCustomer(FormCollection collection)
        {
            return UpdateCustomer();
        }

        //This method is shared between create and save
        private ActionResult UpdateCustomer()
        {
			// Get the updated model
            var model = GetUpdatedModel();

			// Test to see if there are any errors
            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToArray();
            
			//Set flags false
            SetFlagsFalse(model);
			
			// Test to see if the model has validated correctly
            if (ModelState.IsValid)
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                //Attempt update
                try
                {
                    // Map model to data contract
                    CustomerDC CustomerItem = Mapper.Map<CustomerDC>(model.CustomerItem);

					CustomerVMDC returnedObject = null;

                    if (null == model.CustomerItem.Code || model.CustomerItem.Code == Guid.Empty)
                    {
						// Call service to create new Customer item
                        returnedObject = sc.CreateCustomer(CurrentUser, CurrentUser, appID, "", CustomerItem);
                    }
                    else
                    {
						// Call service to update Customer item
                        returnedObject = sc.UpdateCustomer(CurrentUser, CurrentUser, appID, "", CustomerItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdCustomer = returnedObject.CustomerItem;

					// Map data contract to model
                    model.CustomerItem = Mapper.Map<CustomerModel>(createdCustomer);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = CustomerAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.CustomerServiceVersion = model.CustomerItem;
                    sessionManager.CurrentCustomer = model.CustomerItem;
                    
                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
                    model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            return View(model);
        }

        #endregion
		
		#region Exit

        // POST: /Customer/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitCustomer(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentCustomer = null;
                sessionManager.CustomerServiceVersion = null;

                return RedirectToAction("Search", "Customer");
                
            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = Resources.MESSAGE_EXITCONFIRMATION;
                model.IsExitConfirmed = "True";
            }
            
            return View(model);
        }
		
		#endregion
		
		#region Delete

        // POST: /Customer/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteCustomer(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsDeleteConfirmed == "True")
            {
                //Set flags false
                SetFlagsFalse(model);
            
                model.IsDeleteConfirmed = "False";
				
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to delete the item
                    sc.DeleteCustomer(CurrentUser, CurrentUser, appID, "", model.CustomerItem.Code.ToString(), model.CustomerItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentCustomer = null;
                    sessionManager.CustomerServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.CustomerItem = new CustomerModel();
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = CustomerAccessContext.Create;
					
					// Redirect to the search screen
                    return View(model);
                }
				catch (Exception e)
                {
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
                }
            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = Resources.MESSAGE_DELETECONFIRMATION;
                model.IsDeleteConfirmed = "True";
            }
            
            return View(model);
        }
		
		#endregion
		
		#region New

        // POST: /Customer/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewCustomer(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.CustomerCode = null;
            sessionManager.CurrentCustomer = null;
            sessionManager.CustomerServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "Customer");
        }
		
		#endregion

		#region Search

        // GET: /Customer/Search
        //This is called when first entering search Customer screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			CustomerSearchVM model = new CustomerSearchVM();
			
			try
			{
				CustomerSearchVMDC response = sc.SearchCustomer(CurrentUser, CurrentUser, appID, "", null, page, PageSize);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<CustomerSearchMatchDC>, List<CustomerSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.CustomerPageNumber = model.PageNumber;
	        
				return View(model);
			}
			catch (Exception e)
			{
				// Handle the exception
				string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
				model.Message = message;
				
				return View(model);
			}

        }

        // POST: /Customer/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(CustomerSearchVM model, int page = 1)
        {
		
			// Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
				// Test if Select button was clicked...
                if (Key.StartsWith("Search::SearchPost_"))
                {
					// Retrieve ID for entity which was selected
                    Guid Value = Guid.Parse(Key.Substring(19));
									
					// Store ID for Edit screen
					sessionManager.CustomerCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "Customer", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(CustomerVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(CustomerVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private CustomerVM GetUpdatedModel()
        {
            CustomerVM model = new CustomerVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentCustomer != null)
            {
                model.CustomerItem = sessionManager.CurrentCustomer;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentCustomer != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.CustomerItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private CustomerVM ConvertCustomerDC(CustomerVMDC returnedObject)
        {
            CustomerVM model = new CustomerVM();
            
			// Map Customer Item
			model.CustomerItem = Mapper.Map<CustomerDC, CustomerModel>(returnedObject.CustomerItem);
            
			// Map lookup data lists
			model.RelationshipToCustomerList = Mapper.Map<IEnumerable<RelationshipToCustomerDC>, List<RelationshipToCustomerModel>>(returnedObject.RelationshipToCustomerList);
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(CustomerVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            CustomerLookupListsCacheObject CachedLists = cacheManager.CustomerListCache;

			// Retrieve any cached lists to model
			model.RelationshipToCustomerList = CachedLists.RelationshipToCustomerList;
   
        }

        private void MergeNewValuesWithOriginal(CustomerModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            CustomerModel OriginalValuesFromSession = sessionManager.CurrentCustomer;

        }
		
		private void SetAccessContext(CustomerVM model)
        {
            //Decide on access context
            if (null == model.CustomerItem || model.CustomerItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = CustomerAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = CustomerAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(CustomerVM model)
        {
            //Compare the Customer to the original session
            if (model.CustomerItem.PublicInstancePropertiesEqual(sessionManager.CustomerServiceVersion, "RowIdentifier"))
            {
                model.IsViewDirty = false;
            }
            else
            {
                model.IsViewDirty = true;
            }

        }
        #endregion
       
    }
}
