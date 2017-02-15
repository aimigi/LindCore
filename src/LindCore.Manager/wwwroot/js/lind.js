/**
 * Created by Administrator on 2017/2/15.
 意思是，把window对象传入这个匿名函数中，并且同时执行这个函数，在页面载入之前就执行
 小括号有返回值，也就是小括号内的函数或者表达式的返回值，所以说小括号内的function返回值等于小括号的返回值
 */

(function (win) {
    var api = {
        version: '0.0.13',
        init: function (cb) {
            console.log("初始化成功");
            if (typeof cb == "function")
                cb();
        },
        hello: function (lan) {
            if (lan == 0)
                console.log("你好世界!");
            else
                console.log("hello world!");
        }
    };
    win.lind = api;
}(this));

var pubsub = {};
(function (q) {
    var topics = {},
        subUid = -1,
        subscribers,
        len;
    //发布广播事件，包含特定的topic名称和参数  
    q.publish = function (topic, args) {
        if (!topics[topic]) {
            return false;
        }

        subscribers = topics[topic];
        len = subscribers ? subscribers.length : 0;

        while (len--) {
            subscribers[len].func(topic, args);
        }
        return this;
    };
    q.subscribe = function (topic, func) {
        if (!topics[topic]) {
            topics[topic] = [];
        }

        var token = (++subUid).toString();
        topics[topic].push({
            token: token,
            func: func
        });
        return token;
    };

    q.unsubscribe = function (token) {
        for (var m in topics) {
            if (topics[m]) {
                for (var i = 0, j = topics[m].length; i < j; i++) {
                    if (topics[m][i].token === token) {
                        topics[m].splice(i, 1);
                        return token;
                    }
                }
            }
        }
        return this;
    };
})(pubsub);

function log1(topic, data) {
    console.log(topic, data)
}
function log2(topic, data) {
    console.log("Topic is " + topic + " Data is " + data);
}
pubsub.subscribe("主题1", log1);
pubsub.subscribe("主题2", log2);
pubsub.publish("主题1", "aaaaa1");
pubsub.publish("主题1", "aaaaa2");
pubsub.publish("主题2", "ssssss");

(function (win) {
    /**
     * 观察者模式实现事件监听
     */
    function Observer() {
        this._eventsList = {}; // 对外发布的事件列表{"connect" : [{fn : null, scope : null}, {fn : null, scope : null}]}
    }

    Observer.prototype = {
        // 空函数
        _emptyFn: function () {
        },
        /**
         * 判断事件是否已发布
         * @param eType 事件类型
         * @return Boolean
         */
        _hasDispatch: function (eType) {
            eType = (String(eType) || '').toLowerCase();
            return "undefined" !== typeof this._eventsList[eType];
        },

        /**
         * 根据事件类型查对fn所在的索引,如果不存在将返回-1
         * @param eType 事件类型
         * @param fn 事件句柄
         */
        _indexFn: function (eType, fn) {
            if (!this._hasDispatch(eType)) {
                return -1;
            }
            var list = this._eventsList[eType];
            fn = fn || '';
            for (var i = 0; i < list.length; i++) {
                var dict = list[i];
                var _fn = dict.fn || '';
                if (fn.toString() === _fn.toString()) {
                    return i;
                }
            }
            return -1;
        },

        /**
         * 创建委托
         */
        createDelegate: function () {
            var __method = this;
            var args = Array.prototype.slice.call(arguments);
            var object = args.shift();
            return function () {
                return __method.apply(object, args.concat(Array.prototype.slice.call(arguments)));
            }
        },

        /**
         * 发布事件
         */
        dispatchEvent: function () {
            if (arguments.length < 1) {
                return false;
            }
            var args = Array.prototype.slice.call(arguments), _this = this;
            for (var i = 0; i < args.length; i++) {
                var eType = args[i];
                if (_this._hasDispatch(eType)) {
                    return true;
                }
                _this._eventsList[eType.toLowerCase()] = [];
            }
            return this;
        },

        /**
         * 触发事件
         */
        emit: function () {
            if (arguments.length < 1) {
                return false;
            }
            var args = Array.prototype.slice.call(arguments), eType = args.shift().toLowerCase(), _this = this;
            if (this._hasDispatch(eType)) {
                var list = this._eventsList[eType];
                if (!list) {
                    return this;
                }
                for (var i = 0; i < list.length; i++) {
                    var dict = list[i];
                    var fn = dict.fn, scope = dict.scope || _this;
                    if (!fn || "function" !== typeof fn) {
                        fn = _this._emptyFn;
                    }
                    if (true === scope) {
                        scope = null;
                    }
                    fn.apply(scope, args);
                }
            }
            return this;
        },

        /**
         * 订阅事件
         * @param eType 事件类型
         * @param fn 事件句柄
         * @param scope
         */
        on: function (eType, fn, scope) {
            eType = (eType || '').toLowerCase();
            if (!this._hasDispatch(eType)) {
                throw new Error("not dispatch event " + eType);
                return false;
            }
            this._eventsList[eType].push({ fn: fn || null, scope: scope || null });
            return this;
        },

        /**
         * 取消订阅某个事件
         * @param eType 事件类型
         * @param fn 事件句柄
         */
        un: function (eType, fn) {
            eType = (eType || '').toLowerCase();
            if (this._hasDispatch(eType)) {
                var index = this._indexFn(eType, fn);
                if (index > -1) {
                    var list = this._eventsList[eType];
                    list.splice(index, 1);
                }
            }
            return this;
        },

        /**
         * 取消订阅所有事件
         */
        die: function (eType) {
            eType = (eType || '').toLowerCase();
            if (this._eventsList[eType]) {
                this._eventsList[eType] = [];
            }
            return this;
        }
    };

    var cache = {};
    var isReady = false;
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端

    //HTML5开放API
    var api = {

        version: '0.0.13',
        /**
         * 加载完成触发
         * @param cb
         */
        ready: function (cb) {
            eo.on('TsingdaJSBridgeEventOnReady', function () {
                isReady = true;
                cb();
                eo.un('TsingdaJSBridgeEventOnReady');
            });
        },
        /**
         * 配置右上角菜单,传递一个字符串数组
         * WeChatSession 微信好友
         * WeChatTimeline 微信朋友圈
         * TencentQQFriend QQ好友(暂时不支持)
         * TencentQZone  QQ空间(暂时不支持)
         * IntraoralFriend 好班好友
         * IntraoralSession 好班聊天
         * AgencyHomePage 机构首页
         */
        configMenus: function (options) {
            if (!isReady) return;
            options = options || [];
            if (isiOS) {
                window.webkit.messageHandlers.configMenus.postMessage(options);
            } else if (isAndroid) {
                tsingdaJSBridge.configMenus(options);
            }
        },
        /**
         * 设置是否开启自动分享功能
         * 默认是true,客户端点击分享菜单上的按钮后会自动连接第三方工具进行分享
         * 如果设置为false,客户端点击分享菜单上的按钮后，只会触发点击事件,具体的分享有H5调用其它方法完成
         */
        autoShared: function (auto) {
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.autoShared.postMessage(auto);
            } else if (isAndroid) {
                tsingdaJSBridge.autoShared(auto);
            }
        },

        /**
         * 分享到XXX
         */
        sendShared: function (type, options) {
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.sendShared.postMessage({ type: type, options: options });
            } else if (isAndroid) {
                tsingdaJSBridge.sendShared(JSON.stringify({ type: type, options: options }));
            }
            // eo.on('TsingdaJSBridgeEventOnSharedSuccess', success);
            // eo.on('TsingdaJSBridgeEventOnSharedFailure', failure);
        },

        /**
         * 注册分享到朋友圈内容
         * @param type 分享类型,和菜单类型对应
         * @param options JSON对象
         * {
         *    title: "abc", // 分享标题
         *    desc: "哈哈哈",
         *    link: "http://www.baidu.com", // 分享链接
         *    imageUrl: "http://www.baodiu.com/1.jpg", // 分享图标
         * }
         * @param success 分享成功时回调方法
         * @param failure 分享失败时回调方法
         */
        share: function (type, options, success, failure) {
            if (!isReady) return;
            options = options || {};
            cache[type] = {
                data: options,
                success: success,
                failure: failure
            }
        },
        /**
         * 弹出分享菜单
         */
        openShareAlert: function () {
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.openShareAlert.postMessage(null);
            } else if (isAndroid) {
                win.tsingdaJSBridge.openShareAlert();
            }
        },
        /**
         * 支付
         * @param type 支付类型 (wxpay,alipay)
         * @param options 支付配置(待定)
         * 微信支付配置 {
         *    partnerId: "", //商家向财付通申请的商家id
         *    prepayId: "", //预支付订单
         *    nonceStr: "", //随机串，防重发
         *    timeStamp: "", //时间戳，防重发
         *    package: "", //商家根据财付通文档填写的数据和签名
         *    sign: "" //商家根据微信开放平台文档对数据做的签名
         * }
         * @param success  支付完成回调方法,回调会把传进来的参数原路返回,也是一个JSON对象
         * @param failure  支付失败回调方法,会返回出错的字符串信息
         */
        payment: function (type, options, success, failure) {
            if (!isReady) return;
            if (success) {
                eo.on('TsingdaJSBridgeEventOnPaymentSuccess', function (res) {
                    success(res);
                    eo.un('TsingdaJSBridgeEventOnPaymentSuccess');
                });
            }
            if (failure) {
                eo.on('TsingdaJSBridgeEventOnPaymentFailure', function (err) {
                    failure(err);
                    eo.un('TsingdaJSBridgeEventOnPaymentFailure');
                });
            }
            if (isiOS) {
                window.webkit.messageHandlers.payment.postMessage({
                    type: type,
                    options: options
                });
            } else if (isAndroid) {
                win.tsingdaJSBridge.payment(type, JSON.stringify(options || {}));
            }
        },
        /**
         * 日志输出
         */
        log: function (str) {
            console.log(str);
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.log.postMessage(str);
            } else if (isAndroid) {
                win.tsingdaJSBridge.log(str);
            }
        },
        /**
         * 返回上一级页面
         */
        goBack: function () {
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.goBack.postMessage(null);
            } else if (isAndroid) {
                win.tsingdaJSBridge.goBack();
            }
        },
        /**
         * 打开聊天会话
         * @param userID 用户唯一ID
         * @param option 与店主聊天时，自动创建一条产品相关的聊天信息
         * {
         *    title: "abc", // 分享标题
         *    desc: "哈哈哈",
         *    link: "http://www.baidu.com", // 分享链接
         *    imageUrl: "http://www.baodiu.com/1.jpg", // 分享图标
         * }
         */
        openChatSession: function (toUserId, option) {
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.openChatSession.postMessage({ userId: toUserId, option: option || {} });
            } else if (isAndroid) {
                win.tsingdaJSBridge.openChatSession(toUserId, JSON.stringify(option || {}));
            }
        },
        /**
         * 打开登录窗口
         */
        openLogin: function () {
            if (!isReady) return;
            if (isiOS) {
                window.webkit.messageHandlers.openLogin.postMessage(null);
            } else if (isAndroid) {
                win.tsingdaJSBridge.openLogin();
            }
        },
        /**
         * 打开二维码扫描
         * @param successCallback 回调方法
         *
         * @callback 回调方法
         * @param {string} str 二维码对应的字符串
         */
        openQR: function (successCallback) {
            if (!isReady) return;
            if (successCallback) {
                eo.on('TsingdaJSBridgeEventOnQRSuccess', function (str) {
                    successCallback(str);
                    eo.un('TsingdaJSBridgeEventOnQRSuccess');
                });
            }
            if (isiOS) {
                window.webkit.messageHandlers.openQR.postMessage(null);
            } else if (isAndroid) {
                win.tsingdaJSBridge.openQR();
            }
        },
        /**
         * 获取当前位置信息
         * @param callback 回调方法
         *
         * @callback 回调方法
         * @param {JSON} data 获取到的地理位置信息,这是一个JSON对象
         * @param {JSON} data.location  经纬度
         * @param {Number} data.location.longitude 经度
         * @param {Number} data.location.latitude  纬度
         * @param {JSON} data.area            位置信息
         * @param {String} data.area.country  国家
         * @param {String} data.area.city     城市
         * @param {String} data.area.district 区县
         * @param {String} data.area.street   街道
         * @param {String} data.area.name     具体地标
         */
        location: function (callback) {
            if (!isReady) return;
            if (callback) {
                eo.on('TsingdaJSBridgeEventOnLocationSuccess', function (data) {
                    callback(data);
                    eo.un('TsingdaJSBridgeEventOnLocationSuccess');
                });
            }
            if (isiOS) {
                window.webkit.messageHandlers.location.postMessage(null);
            } else if (isAndroid) {
                win.tsingdaJSBridge.location();
            }
        },
        /**
         * 打开系统相册
         * @param maxSelectedCount 最大选择图片数量
         * @param callback 回调方法
         *
         * @callback 回调方法
         * @param {Array} urls 图片上传到服务器后的url地址集合
         */
        openAlbum: function (maxSelectedCount, callback) {
            if (!isReady) return;
            if (callback) {
                eo.on('TsingdaJSBridgeEventOnOpenAlbumSuccess', function (urls) {
                    callback(urls);
                    eo.un('TsingdaJSBridgeEventOnOpenAlbumSuccess');
                });
            }
            if (isiOS) {
                window.webkit.messageHandlers.openAlbum.postMessage(maxSelectedCount);
            } else if (isAndroid) {
                win.tsingdaJSBridge.openAlbum(maxSelectedCount);
            }
        },
        /**
         * 获取设备ID
         * @param callback 回调方法
         * @callback 回调方法
         * @param {String} deviceId 设备ID
         */
        fetchDeviceID: function (callback) {
            if (!isReady) return;
            if (callback) {
                eo.on('TsingdaJSBridgeEventOnFetchDeviceIDSuccess', function (deviceId) {
                    callback(deviceId);
                    eo.un('TsingdaJSBridgeEventOnFetchDeviceIDSuccess');
                });
            }
            if (isiOS) {
                window.webkit.messageHandlers.fetchDeviceID.postMessage(null);
            } else if (isAndroid) {
                win.tsingdaJSBridge.fetchDeviceID();
            }
        }
    };

    //生成事件对象
    var eo = new Observer();
    //添加事件,交互对象已准备完成
    eo.dispatchEvent('TsingdaJSBridgeEventOnReady');
    eo.dispatchEvent('TsingdaJSBridgeEventOnPaymentSuccess');
    eo.dispatchEvent('TsingdaJSBridgeEventOnPaymentFailure');
    eo.dispatchEvent('TsingdaJSBridgeEventOnMenusClickItem');
    eo.dispatchEvent('TsingdaJSBridgeEventOnSharedSuccess');
    eo.dispatchEvent('TsingdaJSBridgeEventOnSharedFailure');
    eo.dispatchEvent('TsingdaJSBridgeEventOnQRSuccess');
    eo.dispatchEvent('TsingdaJSBridgeEventOnLocationSuccess');
    eo.dispatchEvent('TsingdaJSBridgeEventOnOpenAlbumSuccess');
    eo.dispatchEvent('TsingdaJSBridgeEventOnFetchDeviceIDSuccess');

    api.cache = cache;
    api.event = eo;
    win.tsingda = api;
}(this));

// web page : console.log(lind.version);
