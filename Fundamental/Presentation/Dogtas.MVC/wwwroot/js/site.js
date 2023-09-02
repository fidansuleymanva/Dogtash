
/* ---------------------------- $$$ --- GROUPED PRODUCT ADD CART --- $$$ ---------------------------- */

var addToCartGroupedProducts = (element) => {

    //let textValue = $(element).text();

    var textValue = element.innerText || element.textContent;
    $(element).text("Ekleniyor");


    if ($('[groupedproducts]').length) {



        $('[groupedproducts]').first().trigger("click");

        // $('[groupedproducts]').each(function() {
        //         $(this).trigger("click");
        //         return false;   
        //     })
    }

    var fixed = $(element).attr("fixedoverview");
    if(typeof fixed !== typeof undefined && fixed !== false){

        setTimeout(function(){
            $(element).addClass('btn-red-light');
            $(element).empty().append('<i class="shopping-cart-icon icon-cart-kelebek icon price-cell-icon-font"></i><span class="radius-icon"><i class="check-icon check icon custom-icon"></i></span>');
        }, 1500);

    }else{
        setTimeout(function(){
            $(element).text(textValue);

        }, 1500);
    }






};


function urunEklendi(id){

    $("#addCart"+id).text("Ekleniyor");
    setTimeout(function(){ $("#addCart"+id).text("Sepete Ekle");}, 2500);

}




function openflyOut() {
    let closeCallback = {};

    setTimeout(function () {


        if (!$('#flyout-cart').hasClass('active')) {

            $('#flyout-cart').addClass('active');


        }

    },0);

    closeCallback = setTimeout(function(){
        clearTimeout(closeCallback);
        $('#flyout-cart').removeClass('active');
    },3500);




}

function getQuantityOfCarts() {

    $.ajax({
        cache: false,
        url: '/getquantity',
        type: 'post',
        success: function (response) {
            if (response.quantityWish> 0){
                $("#quantityOnWish").show();
                $(".quantityOnWish").text(response.quantityWish);
            }else {
                $("#quantityOnWish").hide();
                $(".quantityOnWish").text(0);
            }

            if(response.quantityCompare>0){
                $("#quantityCompare").show();
                $(".quantityCompare").text(response.quantityCompare);
            }else{
                $("#quantityCompare").hide();
                $(".quantityCompare").text(0);
            }


        }
    });

}

$("span.add-to-wishlist-button").on("click",function(){
    if($(this).find("i").hasClass("active")){
        $(this).find("i").removeClass("active")
    }else{
        $(this).find("i").addClass("active")
    }
});




/* PRODUCT PAGE SHOPPING-ADD-TO-BASKET BUTTON */
$('.shopping-add-to-basket').click(function() {
    if($('.basket-icon').hasClass('shopping cart')){
        $('.basket-icon').removeClass('shopping cart');
        $('.basket-icon').addClass('check');
        $('.basket-text').text('Eklendi');
    } else {
        $('.basket-icon').removeClass('check');
        $('.basket-icon').addClass('shopping cart');
        $('.basket-text').text('Sepete Ekle');
    }
});


//
// /* ---------------------------- $$$ --- HEADER --- $$$ ---------------------------- */
// function searchActionButtonActive (isActive) {
//     if(isActive == true){
//         //  $('.search-action .figure').removeClass('search');
//         //  $('.search-action .figure').addClass('times');
//         $('.header-mega-menu-search-input').focus();
//     }else{
//         // $('.search-action .figure').removeClass('times');
//         // $('.search-action .figure').addClass('search');
//         $('.header-mega-menu-search-input').focusout();
//         $('.header-mega-menu-search-input').val('');
//     }
// }
//
// function cartActionButtonActive (isActive) {
//     if(isActive == true){
//
//         $('.header-shopping-cart-container').focus();
//     }else{
//         $('.header-shopping-cart-container').focusout();
//
//         $('.header-shopping-cart-container').val('');
//     }
// }
//
// var $windowForMenuActionButton = $(window);
// function menuActionButtonActive(isActive) {
//     var windowsize = $windowForMenuActionButton.width();
//     if(isActive == true && windowsize < 600){
//         $('.menu-action .figure').removeClass('bars');
//         $('.menu-action .figure').addClass('times');
//     }else{
//         $('.menu-action .figure').removeClass('times');
//         $('.menu-action .figure').addClass('bars');
//     }
//     if(isActive == true && windowsize < 1400){
//         $('body').addClass('fullpage-mega-content-opened');
//     }else{
//         $('body').removeClass('fullpage-mega-content-opened');
//     }
// }
//
// function menuActionFigureChange(){
//     var windowsize = $windowForMenuActionButton.width();
//     if(windowsize < 600){
//         if($('.menu-action').hasClass('action-active')){
//             menuActionButtonActive(true);
//         }
//     }else{
//         menuActionButtonActive(false);
//     }
//     if(windowsize < 1400 && $('.menu-action').hasClass('action-active')){
//         $('body').addClass('fullpage-mega-content-opened');
//     }else{
//         $('body').removeClass('fullpage-mega-content-opened');
//     }
// };
// menuActionFigureChange();
// $(window).resize(menuActionFigureChange);
//
// function allActionButtonsClose() {
//     $('.mega-content').slideUp(400);
//     $('.nav-action').removeClass('action-active');
//     if($('.search-action .figure').hasClass('times')){
//         searchActionButtonActive(false);
//     }
//     if($('.menu-action .figure').hasClass('times')){
//         menuActionButtonActive(false);
//     }
//     if($('.cart-action .figure').hasClass('times')){
//         cartActionButtonActive(false);
//     }
// }
//
// function openMegaContentByClassName(megaContentClassName, animationCompleteAction) {
//     $('.mega-content').not(megaContentClassName).hide(0).slideUp(0);
//     if(animationCompleteAction == null){
//         $(megaContentClassName).slideToggle(200);
//     }
//     else if(animationCompleteAction == 'setDataForOuterHeight'){
//         $(megaContentClassName).slideToggle(200, function() {
//             $(megaContentClassName).attr('data-outer-height', $(megaContentClassName).outerHeight());
//         });
//     }
// }
//
// $('.nav-action').click(function() {
//     if($(this).hasClass('action-active')){
//         $(this).removeClass('action-active');
//     }else{
//         if($(this).hasClass('has-mega-content')){
//             $(this).addClass('action-active');
//         }
//     }
//     if(!$(this).hasClass('has-mega-content')){
//         $('.mega-content').slideUp(200);
//     }
//     if(!$(this).hasClass('search-action')){
//         searchActionButtonActive(false);
//     }
//     if(!$(this).hasClass('menu-action')){
//         menuActionButtonActive(false);
//     }
//     if(!$(this).hasClass('cart-action')){
//         cartActionButtonActive(false);
//     }
//     $('.nav-action').not(this).removeClass('action-active');
// });
//
// $('.search-action').click(function() {
//     openMegaContentByClassName('.header-search-container');
//     if($(this).hasClass('action-active')){
//         searchActionButtonActive(true);
//     }else{
//         searchActionButtonActive(false);
//     }
// });
//
// $('.menu-action').click(function() {
//     openMegaContentByClassName('.header-mega-menu-container');
//     if($(this).hasClass('action-active')){
//         menuActionButtonActive(true);
//     }else{
//         menuActionButtonActive(false);
//     }
// });
//
// $('.cart-action').click(function() {
//     openMegaContentByClassName('.header-shopping-cart-container');
//     if($(this).hasClass('action-active')){
//         cartActionButtonActive(true);
//     }else{
//         cartActionButtonActive(false);
//     }
// });
//
// $(document).keyup(function(e) {
//     if (e.keyCode == 27) {
//         allActionButtonsClose();
//     }
// });
//
// $(document).click(function(documentClickEvent) {
//     var clickTarget = documentClickEvent.target;
//     if (!$(clickTarget).is('header *') ) {
//         allActionButtonsClose();
//     }
// });


function selectAddressBilling(element){

    $("select#billing-address-select option[value='" + $(element).data("id-bil")+"']").prop('selected', true);
    $('#selectedAddressBilling').empty().append($(element).html());

}



function selectAddressShipping(element){

    $("select#shipping-address-select option[value='" + $(element).data("id-ship")+"']").prop('selected', true);
    $('#selectedAddressShipping').empty().append($(element).html());

}


var billingAdjustAddNew = function () {
    $("select#billing-address-select option[value='0']").prop('selected', true);
    $('#selectedAddressBilling').empty().append($("#billing_0").html());
};

var shippingAdjustAddNew = function () {

    $("select#shipping-address-select option[value='0']").prop('selected', true);
    $('#selectedAddressShipping').empty().append($("#shipping_0").html());


};


function shipToTheSameAddress(el){
    if($(el).is(":checked")){
        $(".billing-delivery-options").show()
    }else{
        $(".billing-delivery-options").hide()
    }
}