
var inputFiles = document.querySelectorAll(".file");
var inputHover = document.querySelectorAll(".hoverFile");
var backgroundImage = document.querySelectorAll(".backgroundImage");
var multiInputFiles = document.querySelectorAll(".multi-file");

console.log(inputHover)

inputHover.forEach(element => {
    var imageBox = document.querySelector(".image-box-hover");


    element.onchange = function (e) {
        let files = e.target.files
        let filesarr = [...files]
        var images = document.querySelectorAll(".image-box-hover img");

        filesarr.forEach(x => {


            if (x.type.startsWith("image/")) {
                let reader = new FileReader()
                reader.onload = function () {
                    let newimg = document.createElement("img")
                    newimg.style.width = "300px";
                    newimg.style.margin = "10px 0px";
                    newimg.setAttribute("src", reader.result)

                    images.forEach(e => e.remove())

                    imageBox.appendChild(newimg)
                }
                reader.readAsDataURL(x)
            }
        })
    }
});


backgroundImage.forEach(element => {
    var imageBox = document.querySelector(".image-box-background");


    element.onchange = function (e) {
        let files = e.target.files
        let filesarr = [...files]
        var images = document.querySelectorAll(".image-box-background img");

        filesarr.forEach(x => {


            if (x.type.startsWith("image/")) {
                let reader = new FileReader()
                reader.onload = function () {
                    let newimg = document.createElement("img")
                    newimg.style.width = "300px";
                    newimg.style.margin = "10px 0px";
                    newimg.setAttribute("src", reader.result)

                    images.forEach(e => e.remove())

                    imageBox.appendChild(newimg)
                }
                reader.readAsDataURL(x)
            }
        })
    }
});

inputFiles.forEach(element => {
    var imageBox = document.querySelector(".image-box");


    element.onchange = function (e) {
        let files = e.target.files
        let filesarr = [...files]
        var images = document.querySelectorAll(".image-box img");

        filesarr.forEach(x => {


            if (x.type.startsWith("image/")) {
                let reader = new FileReader()
                reader.onload = function () {
                    let newimg = document.createElement("img")
                    newimg.style.width = "300px";
                    newimg.style.margin = "10px 0px";
                    newimg.setAttribute("src", reader.result)

                    images.forEach(e => e.remove())

                    imageBox.appendChild(newimg)
                }
                reader.readAsDataURL(x)
            }
        })
    }
});


multiInputFiles.forEach(element => {

    var imageBox = document.querySelector(".image-boxs");

    element.onchange = function (e) {
        let files = e.target.files
        let filesarr = [...files]
        var images = document.querySelectorAll(".image-boxs img");



        filesarr.forEach(x => {

            if (imageBox.classList.contains("removable")) {
                imageBox.innerHTML = "";
            }

            if (x.type.startsWith("image/")) {
                let reader = new FileReader()
                reader.onload = function () {

                    let newimg = document.createElement("img")
                    newimg.style.width = "300px";
                    newimg.style.margin = "10px";
                    newimg.setAttribute("src", reader.result)

                    imageBox.appendChild(newimg)
                    console.log("asd");

                }
                reader.readAsDataURL(x)
            }
        })
    }
});

let dropdownDeliveryItems = document.querySelectorAll(".delivery-list .delivery-btn");

dropdownDeliveryItems.forEach(x => {
    x.addEventListener("click", function (e) {
        e.preventDefault();
        let href = x.getAttribute("href");

        fetch(href).then(response => {
            response.text();
        }).then(data => {

            var element = document.querySelector("#toaster-success")
            var delivery = document.querySelector(".deliverystatus")

            if (element != null) {
                element.value = e.srcElement.getAttribute("value") + " oldu";
                console.log(e.srcElement.getAttribute("value"));

                if (e.srcElement.getAttribute("value") == "İmtina") {
                    delivery.className = "btn bgl-danger text-danger deliverystatus";
                } else if (e.srcElement.getAttribute("value") == "Qəbul") {
                    delivery.className = "btn bgl-success text-success deliverystatus";
                } else {
                    delivery.className = "btn bgl-warning text-warning deliverystatus";
                }

                delivery.innerHTML = e.srcElement.getAttribute("value");
            } else {
                const success = document.createElement("input");
                success.id = "toaster-success";
                success.innerHTML = "adfafd";
                success.value = e.srcElement.getAttribute("value") + " oldu";

                if (e.srcElement.getAttribute("value") == "İmtina") {
                    delivery.className = "btn bgl-danger text-danger deliverystatus";
                } else if (e.srcElement.getAttribute("value") == "Qəbul") {
                    delivery.className = "btn bgl-success text-success deliverystatus";
                } else {
                    delivery.className = "btn bgl-warning text-warning deliverystatus";
                }

                delivery.innerHTML = e.srcElement.getAttribute("value");
                document.querySelector("body").appendChild(success);
                console.log(e.srcElement.getAttribute("value"));
            }

            if ($("#toaster-success").length) {
                toastr["success"]($("#toaster-success").val())
            }


        })
    });
});



let imageCollections = document.querySelectorAll(".imageCollection");

imageCollections.forEach(x => {

    x.addEventListener("click", function (e) {
        console.log();
        var id = x.getAttribute("value");
        console.log(id);

        fetch("/partnerProduct/deleteImage/" + id).then(response => {
            if (!response.ok) {
                throw new Error('İstekte bir hata oluştu!');
            }
            response.text();
        })
            .then(data => {
                console.log(x.parentElement);
                x.parentElement.remove();
                if ($("#toaster-success").length) {
                    toastr["success"]($("#toaster-success").val())
                }
            })
    })
})


let languageSelect = document.querySelector(".languageSelect")
let categorySelect = document.querySelector(".categorySelect")
console.log(languageSelect)

languageSelect.addEventListener("click", function (e) {

    console.log(e)


    var action = e.srcElement.getAttribute("data-action")
    var eClass = e.srcElement.getAttribute("data-eClass")

    fetch(`/manage/${action}/getAll?languageId=${e.target.value}`)
        .then(response => response.text())
        .then(data => {
            console.log(data);
            var list = JSON.parse(data)
            document.querySelector(`.${eClass}`).innerHTML = "";

            list.forEach(x => {
                let option = document.createElement("option");
                option.value = x.id;
                option.text = x.name;
                document.querySelector(`.${eClass}`).append(option);
            })

            console.log(list);
        })
})

categorySelect.addEventListener("click", function (e) {

    console.log(e.target.value);
    console.log(categorySelect);
    const languageId = document.querySelector(".languageSelect").value;

    fetch(`/manage/subcategory/GetAll?languageId=${languageId}&categoryId=${e.target.value}`)
        .then(response => response.text())
        .then(data => {
            console.log(data);
            var list = JSON.parse(data)
            document.querySelector(".subCategorySelect").innerHTML = "";

            list.forEach(x => {
                let option = document.createElement("option");
                option.value = x.id;
                option.text = x.name;
                document.querySelector(".subCategorySelect").append(option);
            })

            console.log(list);
        })
})


//document.addEventListener("DOMContentLoaded", function () {
//    const qeydEtButton = document.getElementById("qeydEtButton");

//    qeydEtButton.addEventListener("click", function () {
//        // Toastr bildirimini göster
//        toastr["success"]("Məlumat uğurla qeyd edildi!");

//        // Toastr seçeneklerini belirle
//        toastr.options = {
//            "closeButton": false,
//            "debug": false,
//            "newestOnTop": false,
//            "progressBar": false,
//            "positionClass": "toast-top-center", // Değişiklik burada
//            "preventDuplicates": false,
//            "onclick": null,
//            "showDuration": "300",
//            "hideDuration": "1000",
//            "timeOut": "5000",
//            "extendedTimeOut": "1000",
//            "showEasing": "swing",
//            "hideEasing": "linear",
//            "showMethod": "fadeIn",
//            "hideMethod": "fadeOut"
//        };
//    });
//});
