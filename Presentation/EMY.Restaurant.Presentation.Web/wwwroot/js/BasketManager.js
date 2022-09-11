function removeStorage(name) {
    try {
        localStorage.removeItem(name);
        localStorage.removeItem(name + '_expiresIn');
    } catch (e) {
        console.log('removeStorage: Error removing key [' + key + '] from localStorage: ' + JSON.stringify(e));
        return false;
    }
    return true;
}

function getStorage(key) {

    var now = Date.now();
    var expiresIn = localStorage.getItem(key + '_expiresIn');
    if (expiresIn === undefined || expiresIn === null) { expiresIn = 0; }

    if (expiresIn < now) {
        removeStorage(key);
        return null;
    } else {
        try {
            var value = localStorage.getItem(key);
            return value;
        } catch (e) {
            console.log('getStorage: Error reading key [' + key + '] from localStorage: ' + JSON.stringify(e));
            return null;
        }
    }
}

function setStorage(key, value, expires) {

    if (expires === undefined || expires === null) {
        expires = 30;  // default: seconds for 1 day
    } else {
        expires = Math.abs(expires); //make sure it's positive
    }

    var now = Date.now();  //millisecs since epoch time, lets deal only with integer
    var schedule = now + expires * 1000 * 60;
    try {
        localStorage.setItem(key, value);
        localStorage.setItem(key + '_expiresIn', schedule);
    } catch (e) {
        console.log('setStorage: Error setting key [' + key + '] in localStorage: ' + JSON.stringify(e));
        return false;
    }
    return true;
}

function getBasketItems() {
    return getStorage('basket');
}

function setBasketItems(obj) {
    setStorage('basket', obj, 30);
}

function addToBasket(productID, productCode, productName, price, photoUrl) {

    var newItem = {
        productid: productID,
        productCode: productCode,
        productName: productName,
        count: 1,
        price: price,
        photourl: photoUrl
    };
    finded = false;
    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    for (var i = 0; i < basketItems.length; i++) {
        if (basketItems[i]['productid'] === productID) {
            basketItems[i].count = basketItems[i].count + 1;
            finded = true;
            break;
        }
    }
    if (!finded) {
        basketItems.push(newItem);
    }
    setBasketItems(JSON.stringify(basketItems));
}

function incrisebasket(id) {
    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    for (var i = 0; i < basketItems.length; i++) {
        if (basketItems[i]['productid'] === id) {
            basketItems[i].count = basketItems[i].count + 1;
            break;
        }
    }
    setBasketItems(JSON.stringify(basketItems));
}

function decrisebasket(id) {
    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    for (var i = 0; i < basketItems.length; i++) {
        if (basketItems[i]['productid'] === id) {
            if (basketItems[i].count - 1 < 1) {
                removeBasketItem(id);
                return;
            }
            basketItems[i].count = basketItems[i].count - 1;
            break;
        }
    }
    setBasketItems(JSON.stringify(basketItems));
}

function removeBasketItem(productID) {
    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    basketItems = basketItems.filter(o => o.productid != productID)
    setBasketItems(JSON.stringify(basketItems));
}


function reloadShopCart() {

    document.getElementById("tbdy").innerHTML = "";
    var objs = `<tr>
                                                <td>
                                                    <div class="thumb_cart">
                                                        <img src="/Uploads/Photos/$$img" data-src="/Uploads/Photos/$$img" class="lazy loaded" alt="Image" data-was-processed="true">
                                                    </div>
                                                    <span data-descfoodid='$$id' class="item_cart">$$cntx $$name</span>
                                                </td>
                                                <td>
                                                    <strong>€$$baseprice</strong>
                                                </td>
                                                <td>
                                                    <div class="numbers-row">
                                                        <input type="text" data-foodid='$$id' value="$$cnt" id="quantity_1" class="qty2" name="quantity_1">
                                                        <div onclick="incrisebasket('$$id'); reloadShopCart();" class="inc button_inc">+</div>
                                                        <div onclick="decrisebasket('$$id'); reloadShopCart();" class="dec button_inc">-</div>
                                                        <div onclick="incrisebasket('$$id'); reloadShopCart();" class="inc button_inc">+</div>
                                                        <div onclick="decrisebasket('$$id'); reloadShopCart();" class="dec button_inc">-</div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <strong>€$$price</strong>
                                                </td>
                                                <td class="options">
                                                    <a onclick="remove('$$id')"><i class="ti-trash"></i></a>
                                                </td>
                                            </tr>`;

    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    totalprice = 0.0;
    basketItems.forEach(function (item) {
        var obj = objs.replace(/\$\$img/g, item.photourl);
        obj = obj.replace(/\$\$id/g, item.productid);
        obj = obj.replace(/\$\$cnt/g, item.count);
        obj = obj.replace(/\$\$name/g, item.productName);
        obj = obj.replace(/\$\$baseprice/g, parseFloat(item.price));
        obj = obj.replace(/\$\$price/g, parseFloat(item.price) * item.count);
        totalprice += parseFloat(item.price) * item.count;
        document.getElementById("tbdy").innerHTML += obj;
    });
    document.getElementById('summarysubtotal').textContent = '€' + totalprice;
    document.getElementById('summarytotalprice').textContent = '€' + (totalprice);
    loadHeaderOrder();
}
function loadHeaderOrder() {
    document.getElementById("headerOrder").innerHTML = "";
    var itemcount = 0;
    var totalBasketPrice = 0.0;
    var content = `<li>
                       <figure><img src="/Uploads/Photos/$$img" data-src="/Uploads/Photos/$$img" alt="" width="50" height="50" class="lazy loaded" data-was-processed="true"></figure>
                       <strong><span>$$cntx $$name</span>€$$price</strong>
                       <a href="#0" onclick="removeBasketItem('$$id'); loadHeaderOrder();" class="action"><i class="icon_trash_alt"></i></a>
                   </li>`;
    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    basketItems.forEach(function (item) {
        var obj = content.replace(/\$\$img/g, item.photourl);
        obj = obj.replace(/\$\$id/g, item.productid);
        obj = obj.replace(/\$\$cnt/g, item.count);
        obj = obj.replace(/\$\$name/g, item.productName);
        obj = obj.replace(/\$\$price/g, parseFloat(item.price) * item.count);
        totalBasketPrice += parseFloat(item.price) * item.count;
        itemcount++;
        document.getElementById("headerOrder").innerHTML += obj;

    });
    document.getElementById("totalheaderitemcount").textContent = itemcount;
    document.getElementById("totalBasketPrice").textContent = '€' + totalBasketPrice;

}

function loadCheckOutPage() {
    var itemcount = 0;
    var totalBasketPrice = 0.0;

    var content = `<li><a href="#0" onclick="removeBasketItem('$$id');loadCheckOutPage();loadHeaderOrder();">$$cntx $$name</a><span>€$$price</span></li>`;


    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);
    document.getElementById("checkoutList").innerHTML = "";
    basketItems.forEach(function (item) {

        var obj = content.replace(/\$\$id/g, item.productid);
        obj = obj.replace(/\$\$cnt/g, item.count);
        obj = obj.replace(/\$\$name/g, item.productName);
        obj = obj.replace(/\$\$price/g, parseFloat(item.price) * item.count);
        document.getElementById("checkoutList").innerHTML += obj;
        totalBasketPrice += parseFloat(item.price) * item.count;
        itemcount++;
    });

    document.getElementById("summarySubtotal").textContent = '€' + totalBasketPrice;
    document.getElementById("summaryTotal").textContent = '€' + totalBasketPrice;

}


function confirmOrder() {
    var cookieItems = getBasketItems();
    var basketItems = [];
    if (cookieItems != null && cookieItems.length > 0)
        basketItems = JSON.parse(cookieItems);

    var products = [];
    if (basketItems.length == 0) {
        MessageBox.Show('Error.', 'Please add products to your basket.');
    }
    basketItems.forEach(function (item) {
        products.push({ productid: item.productid, count: item.count });
    });

    var constradioButtons = document.querySelectorAll('input[name="payment_method"]');
    var payment_method = 'CCard';
    constradioButtons.forEach(function (rb) { if (rb.checked) payment_method = rb.value; });



    var fullName = document.getElementById('fullName').value;
    if (fullName.length == 0) {
        MessageBox.Show('Error', 'Please fill your full name.');
        return;
    }
    var email = document.getElementById('email').value;
    var phone = document.getElementById('phone').value;
    var notes = document.getElementById('notes').value;
    if (phone.length == 0) {
        MessageBox.Show('Error', 'Please fill your phone number.');
        return;
    }
    var fullAdress = document.getElementById('fullAdress').value;
    if (fullAdress.length == 0) {
        MessageBox.Show('Error', 'Please fill your full adress.');
        return;
    }
    var city = document.getElementById('city').value;
    if (city.length == 0) {
        MessageBox.Show('Error', 'Please fill your city.');
        return;
    }

    var postalCode = document.getElementById('postalCode').value;
    if (postalCode.length == 0) {
        MessageBox.Show('Error', 'Please fill your postal code.');
        return;
    }

    MessageBox.AjaxPost('/Home/CreateOrder',
        {
            productsJson: JSON.stringify(products),
            payment_method: payment_method,
            fullName: fullName,
            email: email,
            phone: phone,
            fullAdress: fullAdress,
            city: city,
            postalCode: postalCode,
            notes: notes
        },
        function (data) {
            basketItems.forEach(function (item) {
                removeBasketItem(item.productid);
            });
            console.log('data=', data)
            location.href = 'Confirmation?id=' + data;
        }, function (data) {
            MessageBox.Show('Error creating order', 'error');
        });

}

loadHeaderOrder();
if (shopcardflag) reloadShopCart();
if (checkoutflag) loadCheckOutPage();