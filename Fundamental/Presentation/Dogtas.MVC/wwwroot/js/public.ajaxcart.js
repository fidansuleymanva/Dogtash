/*
** nopCommerce ajax cart implementation
*/


var AjaxCart = {
    loadWaiting: false,
    redirect: false,
    usepopupnotifications: false,
    topcartselector: '',
    topwishlistselector: '',
    flyoutcartselector: '',

 
    init: function (usepopupnotifications, topcartselector, topwishlistselector, flyoutcartselector, shouldredirect) {
        this.loadWaiting = false;
        this.usepopupnotifications = usepopupnotifications;
        this.topcartselector = topcartselector;
        this.topwishlistselector = topwishlistselector;
        this.flyoutcartselector = flyoutcartselector;
        this.redirect = shouldredirect;
    },

    setLoadWaiting: function (display) {
        displayAjaxLoading(display);
        this.loadWaiting = display;
    },

    //add a product to the cart/wishlist from the catalog pages
    addproducttocart_catalog: function (urladd, formselector) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: urladd,
            data: $(formselector).serialize(),
            type: "POST",
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    //add a product to the cart/wishlist from the product details page
    addproducttocart_details: function (urladd, formselector) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: urladd,
            data: $(formselector).serialize(),
            type: "POST",
            success: this.success_process_detail,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    //add a product to compare list
    addproducttocomparelist: function (urladd) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: urladd,
            type: "POST",
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    success_process: function (response) {

       // getQuantityOfCarts();
        if (response.updatetopcartsectionhtml) {
            $(AjaxCart.topcartselector).html(response.updatetopcartsectionhtml);
            $(".fly-out-quantity-text").text( response.updatetopcartsectionhtml);
        }
        if (response.updatetopwishlistsectionhtml) {
            $(AjaxCart.topwishlistselector).html(response.updatetopwishlistsectionhtml);
            $(".wishlist-quantity-text").text(response.updatetopwishlistsectionhtml);
        }
        
        if (response.updateflyoutcartsectionhtml) {
            $(AjaxCart.flyoutcartselector).replaceWith(response.updateflyoutcartsectionhtml);
            openflyOut();
            
            //Start -- quin AI Listing page add to Cart
            try {
                console.log("quin AI Listing page add to Cart");
               
                if (response.quinAiModel) {
                    console.log(response.quinAiModel.Value)
                    //for every item generate geralt track
                     var responseQuinModel = response.quinAiModel.Value;
                        var item = responseQuinModel.item;
                        var value = responseQuinModel.value;
                        console.log(item,value);
                        geralt.track('click', {
                            category: 'listing',
                            label: 'addtobasket',
                            item:item,
                            value: value
                        });
                    
                }
                else{
                    console.log("Quin model undefined!");
                }
            } catch {
                console.log("Quin Listing addtobasket click not working!");
            }
            //End -- quin AI Listing page add to Cart
            
            try {
                if (response.addedProducts) {
                    fbq('track', 'AddToCart', {
                        'currency': 'TRY',
                        'value': response.addedProducts.Value.Value,
                        'content_ids': response.addedProducts.Value.SkuList
                    });
                }
            } catch {
                console.log("Fbq not working!");
            }
        }
        if (response.message) {
            //display notification
            if (response.success == true) {
                //success
               
                
              //  var newFlyOutQuantity = $("#fly-out-quantity").val();
                
                if (AjaxCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'success', true);
                }
                else {
                    //specify timeout for success messages
                    displayBarNotification(response.message, 'success', 3500);
                }
            }
            else {
                //error
                if (AjaxCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'error', true);
                }
                else {
                    //no timeout for errors
                    displayBarNotification(response.message, 'error', 0);
                }
            }
            return false;
        }
        if (response.redirect && AjaxCart.redirect) {
            
            location.href = response.redirect;
            return true;
        }
       return false;
    },

    success_process_detail: function (response) {
        if (response.updatetopwishlistsectionhtml) {
            $(AjaxCart.topwishlistselector).html(response.updatetopwishlistsectionhtml);
            $(".wishlist-quantity-text").text(response.updatetopwishlistsectionhtml);
            
            try {
                console.log("quin AI Detail and Listing page add to wishlist");
                if (response.quinAiModel) {
                    var responseQuinModel = response.quinAiModel.Value;
                    var item = responseQuinModel.item;
                    var value = responseQuinModel.value;
                    console.log(item,value);

                    geralt.track('click', {
                        category: 'detail',
                        label: 'addtofavourites',
                        item: item,
                        value: value
                    });

                }
            } catch {
                console.log("Quin detail wishlist click not working!")
            }
            //End -- quin AI Detail page add to Cart
            

        }
        if (response.updateflyoutcartsectionhtml) {
            $(AjaxCart.flyoutcartselector).replaceWith(response.updateflyoutcartsectionhtml);
            openflyOut();
            //Start -- quin AI Detail page add to Cart
            try {
                console.log("quin AI Detail page add to Cart");
                if (response.quinAiModel) {
                    var responseQuinModel = response.quinAiModel.Value;
                    var item = responseQuinModel.item;
                    var value = responseQuinModel.value;
                        console.log(item,value);

                        geralt.track('click', {
                            category: 'detail',
                            label: 'addtobasket',
                            item:item,
                            value: value
                        });
                    
                }
            } catch {
                console.log("Quin detail addtobasket click not working!")
            }
            //End -- quin AI Detail page add to Cart

            try {
                if (response.addedProducts) {
                    fbq('track', 'AddToCart', {
                        'currency': 'TRY',
                        'value': response.addedProducts.Value.Value,
                        'content_ids': response.addedProducts.Value.SkuList
                    });
                }
            } catch {
                console.log("Fbq not working!")
            }
        }
        if (response.message) {
            //display notification
            if (response.success == true) {
                //success
                
                //  var newFlyOutQuantity = $("#fly-out-quantity").val();
                
                if (AjaxCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'success', true);
                }
                else {
                    //specify timeout for success messages
                    displayBarNotification(response.message, 'success', 3500);
                }
            }
            else {
                //error
                if (AjaxCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'error', true);
                }
                else {
                    //no timeout for errors
                    displayBarNotification(response.message, 'error', 0);
                }
            }
            return false;
        }
        if (response.redirect && AjaxCart.redirect) {

            location.href = response.redirect;
            return true;
        }
        return false;
    },

    resetLoadWaiting: function () {

        AjaxCart.setLoadWaiting(false);
    },

    ajaxFailure: function () {
        alert('Ürün sepete eklenemedi. Lütfen sayfayı yenileyip tekrar deneyiniz.');
    }
};