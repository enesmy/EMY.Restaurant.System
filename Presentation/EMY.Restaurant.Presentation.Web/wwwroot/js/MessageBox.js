class MessageBox {

    static ym = null;
    static nm = null;
    static sm = null;
    static sleeptime = 350;

    static Show(Header, Message) {
        this.IsQuestion(false);
        document.getElementById('messageboxModalLabel').textContent = Header;
        document.getElementById('messageboxModalContent').textContent = Message;
        this.nm = null;
        $("#MessageboxModal").modal('show');
    }

    static ShowHtml(Header, Html, SaveMethod) {
        console.log('this.ShowHtml');
        this.IsQuestion(false);
        document.getElementById('messageboxModalLabel').textContent = Header;
        document.getElementById('messageboxModalContent').innerHTML = Html;
        this.sm = SaveMethod;
        this.IsHtml(true);
        $("#MessageboxModal").modal('show');
    }

    static AjaxGet(header, pageUrl, parameters, saveMethod,successMethod, errorMethod) {
        let htmlContent = "";
        $.ajax({
            url: pageUrl,
            type: "GET",
            data: parameters,
            success: function (data) {
                MessageBox.ShowHtml(header, data, saveMethod);
                if (successMethod != null) successMethod();
            },
            error: function (data) {
                MessageBox.ShowMessage("Error: " + data.statusText);
                if (errorMethod != null) errorMethod();
            }

        });
    }

    static AjaxPost(pageUrl, parameters, success, error) {
        
        $.ajax({
            url: pageUrl,
            type: "POST",
            data: parameters,
            success: success,
            error: error
        });
    }

    static ShowMessage(Message) {
        document.getElementById('messageboxModalLabel').textContent = document.title;
        document.getElementById('messageboxModalContent').textContent = Message;
        this.IsQuestion(false);
        this.nm = null;
        $("#MessageboxModal").modal('show');
    }

    static AskYesNo(Header, Message, YesMethod, NoMethod)
    {
        document.getElementById('messageboxModalLabel').textContent = Header;
        document.getElementById('messageboxModalContent').textContent = Message;
        this.IsQuestion(true);
        this.ym = YesMethod;
        this.nm = NoMethod;

        $("#MessageboxModal").modal('show');
    }
    
    static AskYesNoHtml(Header, Message, YesMethod, NoMethod) {
        document.getElementById('messageboxModalLabel').textContent = Header;
        document.getElementById('messageboxModalContent').innerHTML = Message;
        this.IsQuestion(true);
        this.ym = YesMethod;
        this.nm = NoMethod;

        $("#MessageboxModal").modal('show');
    }
    
    static AskAccept(Header, Message, Yesmethod) {
        document.getElementById('messageboxModalLabel').textContent = Header;
        document.getElementById('messageboxModalContent').textContent = Message;
        this.IsQuestion(true);
        this.ym = Yesmethod;
        $("#MessageboxModal").modal('show');
    }
    
    static AskAcceptHtml(Header, Message, Yesmethod) {
        document.getElementById('messageboxModalLabel').textContent = Header;
        document.getElementById('messageboxModalContent').innerHTML = Message;
        this.IsQuestion(true);
        this.ym = Yesmethod;
        $("#MessageboxModal").modal('show');
    }

    static IsQuestion(answer) {
        document.getElementById('modalYes').hidden = !answer;
        document.getElementById('modalNo').hidden = !answer;
        document.getElementById('modalClose').hidden = answer;
        document.getElementById('modalSave').hidden = true;
        this.clear();
    }

    static IsHtml(answer) {
        document.getElementById('modalYes').hidden = answer;
        document.getElementById('modalNo').hidden = answer;

        document.getElementById('modalSave').hidden = !answer;
        document.getElementById('modalClose').hidden = !answer;
        this.clear();
    }

    static async yesClicked() {
        await this.sleep(this.sleeptime);
        if (this.ym != null) this.ym();
    }

    static async noClicked() {
        await this.sleep(this.sleeptime);
        if (this.nm != null) this.nm();
    }

    static async saveClicked() {
        await this.sleep(this.sleeptime);
        if (this.sm != null) this.sm();
    }

    static clear() {
        this.ym = null;
        this.nm = null;
    }

    static sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}