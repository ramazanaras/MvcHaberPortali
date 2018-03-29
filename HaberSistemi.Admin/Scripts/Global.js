function KategoriEkle() {


    //nesne
    Kategori = new Object();
    Kategori.KategoriAdi = $("#KategoriAdi").val();  // Kategori.KategoriAdi deki KategoriAdi kısmı modeldekiyle aynı olmalı
    Kategori.URL = $("#KategoriUrl").val();
    Kategori.AktifMi = $("#kategoriAktif").is(":checked");
    Kategori.ParentID = $("#ParentID").val();

    //console.log(Kategori);

    //ajaxla veri ekliyoruz
    $.ajax({
        url: "/Kategori/Ekle",
        data: Kategori, //model ile aynı propertiler aynı olmalı
        type: "POST",
        dataType:"json",
        success: function (response) { // controllerdan dönen veri -->response  // //ResultJson sınıfı tipinde  gelcek controllerdan  
            if(response.Success){ 
                //bootbox.js (modal popup)

                bootbox.alert(response.Message, function () {
                    location.reload();//sayfayı yenile
                });


            } else {
                bootbox.alert(response.Message, function () {
                  //geri döndüğünde bir şey yapılması isteniyorsa buraya yazılır
                });
            }
        }



    });






}



function KategoriSil(a) {
  
    var gelenID = $(a).data("id");

    //var silTr = $(this).closest("tr");//silinecek satır
    alert(gelenID);



    $.ajax({

        url: "/Kategori/Sil/" + gelenID,
        type: "POST",
        dataType: "json",
        succcess: function (response) {//controllerdan gelen veri response

            alert("mesajjjjj")
            if (response.Succcess) {
                //modal olarak mesaj  gösterir
                //bootbox.alert(response.Message, function () {
                //    location.reload();//sayfayı yenile
                //});

                //3 sn bekle
                //silTr.fadeOut(300, function () {
                //    silTr.remove();//satırı sil
                //})
              

                //mesaj verdik
                $.notify(response.Message,"success");





            } else {
                //bootbox.alert(response.Message, function () {
                // //geri döndüğünde bir şey yaptırabiliriz.
                //});




            }

        }


    });




}



//$(document).on("click", "#KategoriDelete", function () {

//    var gelenID = $(this).attr("data-id");
    
//    $.ajax({

//        url: "/Kategori/Sil/" + gelenID,
//        type: "POST",
//        dataType: "json",
//        succcess: function (response) {//controllerdan gelen veri response


//            if (response.Succcess) {
//                //modal olarak mesaj  gösterir
//                bootbox.alert(response.Message, function () {
//                    location.reload();//sayfayı yenile
//                });
//            } else {
//                bootbox.alert(response.Message, function () {
//                    //geri döndüğünde bir şey yaptırabiliriz.
//                });
//            }

//        }


//    });


//});



function KategoriDuzenle(gelenID) {

    //alert(gelenID);


    //nesne
    Kategori = new Object();
    Kategori.KategoriAdi = $("#KategoriAdi").val();  // Kategori.KategoriAdi deki KategoriAdi kısmı modeldekiyle aynı olmalı
    Kategori.URL = $("#KategoriUrl").val();
    Kategori.AktifMi = $("#kategoriAktif").is(":checked");
    Kategori.ParentID = $("#ParentID").val();
    Kategori.ID = gelenID;

    //console.log(Kategori);

    //ajaxla veri ekliyoruz
    $.ajax({
        url: "/Kategori/Duzenle",
        data: Kategori, //model ile aynı propertiler aynı olmalı
        type: "POST",
        dataType: "json",
        success: function (response) { // controllerdan dönen veri -->response  // //ResultJson sınıfı tipinde  gelcek controllerdan  
            if (response.Success) {
                //bootbox.js (modal popup)

                bootbox.alert(response.Message, function () {
                    location.reload();//sayfayı yenile
                });


            } else {
                bootbox.alert(response.Message, function () {
                    //geri döndüğünde bir şey yapılması isteniyorsa buraya yazılır
                });
            }
        }



    });




}