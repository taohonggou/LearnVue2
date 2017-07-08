# 学习Vue笔记

## Vue 介绍

### 声明是渲染

1. 通过vue绑定的所有元素都是响应式的。我们通过控制台修改vue对象的中的值，对应的DOM元素的值也会变化。

2. 通过`v-bind`绑定DOM元素的属性，这是不用加`{{}}`，代码如下

   ```html
   <div id="app-2">
     <!--这里的message没有加{{}}-->
     <span v-bind:title="message">
       鼠标悬停几秒钟查看此处动态绑定的提示信息！
     </span>
   </div>
   ```

   `v-bind`被称为**指令**

### 条件与循环

1. 条件`v-if`指令，当`v-if`绑定的值为`true`是显示

   ```html
   <div v-if="show">
   	你能看见我
   </div>
   <!--当Vue对象中的show为true是显示，在控制台改变show的值，div也会跟着显示和隐藏-->
   ```

   从这个例子可以看出，vue不仅可以绑定DOM文本，还可以绑定DOM结构，而且，Vue 也提供一个强大的过渡效果系统，可以在 Vue 插入/更新/删除元素时自动应用[过渡效果](https://cn.vuejs.org/v2/guide/transitions.html)。

2. `v-for`循环

   ```html
           <select>
               <option>全部</option>
               <option v-for="city in citys" v-bind:value="city.id">{{city.cityName}}</option>
           </select>
   ```

   `v-for`是循环自身，那个标签上有`v-for`就会循环那个标签

### 处理用户输入  

1. `v-on`指令绑定事件监听器，通过他调用vue实例中定义的方法

   ~~~html
       <div>
           <p>{{userInput}}</p>
         	<!--reverseMessage不用加（）-->
           <button v-on:click="reverseMessage">翻转文字</button>
       </div>
   ~~~

   ~~~javascript
   var app = new Vue({
           el: '#app',
           data: {
               userInput:'大家好！',
           },
     		//vue所有的方法都放在这里
           methods: {
               reverseMessage: function () {
                   this.userInput = this.userInput.split('').reverse().join('');
               }
           }
       });
   ~~~

   在上面的代码中我们更新了DOM的状态，但是我们并没有写代码操作DOM--所有的DOM操作都由vue来处理。

2. `v-model`实现双向绑定

   ~~~html
       <div>
           <p>{{learn_v_model}}</p>
           <input type="text" name="name" v-model="learn_v_model" />
       </div>
   ~~~

   当我们在改变input的值时，p标签中的文字也会实时变化

### 组件

1. 没有参数的组件

   ```javascript
   // 定义名为 todo-item 的新组件
   Vue.component('todo-item', {  //todo-item是组件名称
     template: '<li>这是个待办项</li>'
   })	
   ```

   组件的位置是在创建vue对象前面，使用代码如下：

   ```html
   <ol>
       <!--todo-item就是组件的名称-->
   	<todo-item></todo-item>
   </ol>
   ```

2. 有参数的组件

   ~~~javascript
       Vue.component('todo-item-param',{
           props:['todo'],
           template:'<li>{{todo.text}}</li>'
       });
   ~~~

   使用

   ~~~html
               <todo-item-param 
                                v-for="item in groceryList"
                                v-bind:todo="item"    
                                v-bind:key="item.id"  
                                >
               </todo-item-param>
   <!--通过v-bind:todo给组件的属性赋值-->
   ~~~

## Vue 实例

### 属性与方法

每个vue实例都会*代理*其`data`对象所有属性

~~~javascript
var data={a:1};
var vm=new Vue({
	data:data,
});
vm.a===data.a  //->true
~~~

注意只有这些被代理的属性是**响应的**。如果在实例创建之后添加新的属性到实例上，它不会触发视图更新。

我们可以使用 `$`来直接使用vue对象中的属性，例如：

~~~javascript
var data={a:1};

var vm=new Vue({
    el:'#app',
    data:data
});

var elResult= vm.$el===document.getElementById('app');
var dataResult=vm.$data===data;

//这个回调将在 `vm.a`  改变后调用
vm.$watch('a',function(newVal,oldVal){
    console.log('newVal='+newVal+'；oldVal='+oldVal);
});
~~~

### 实例生命周期

以下是实例生命周期图示

![Lifecycle](https://cn.vuejs.org/images/lifecycle.png)

使用如下

```javascript
var vm = new Vue({
  data: {
    a: 1
  },
  //是直接写在vm对象下面的
  created: function () {
    // `this` 指向 vm 实例
    console.log('a is: ' + this.a)
  }
})
// -> "a is: 1"
```

## 模板语法

### 插值

1. 文本

   常见形式“Mustache” 语法（双大括号）

   使用`v-once`指令，可以实现一次性的赋值，当模型的值发生变化时页面不变

   ~~~html
           <p v-once>
               不会变化：{{testOnce}}
           </p>
           <!--当我们改变testOnce的值时，页面不会发生变化-->
   ~~~

2. 纯html

   使用 `v-html` 指令可以直接输出html

   ~~~HTML
   <div v-html="rawHtml"></div>
   ~~~

   ~~~javascript
   rawHtml:'<p>my is p</p>'
   //这是vue对象data中的值
   ~~~

   最后页面的效果如下

   ~~~html
   <div>
     <p>my is p</p>
   </div>
   ~~~

3. 属性

   Mustache 不能在 HTML 属性中使用，应使用 [v-bind 指令](https://cn.vuejs.org/v2/api/#v-bind)：

   ~~~html
   <label v-bind:class="labelClass">标签</label>
   ~~~

   这对布尔值的属性也有效 —— 如果条件被求值为 false 的话该属性会被移除：

   ~~~html
   <button v-bind:disabled="isButtonDisabled" class="btn btn-primary">按钮</button>
   ~~~

4. 使用JavaScript表示

   ~~~html
   <label class="label" v-bind:class="isReView==1?'label-danger':'label-primary'" v-bind:id="'label'+labelId">标签</label>
   <p>{{computers.split(',').join('+')}}</p>
   ~~~

   有个限制就是，每个绑定都只能包含**单个表达式**，所以下面的例子都**不会**生效。

   ~~~html
   <!-- 这是语句，不是表达式 -->
   {{ var a = 1 }}
   <!-- 流控制也不会生效，请使用三元表达式 -->
   {{ if (ok) { return message } }}
   ~~~

### 指令

1. 参数

   一些指令能接受一个参数，在指令后以**冒号**声明。

   ~~~html
   <a v-bind:href="url">my is a</a>
   ~~~

   另一个例子是 `v-on` 指令，它用于监听 DOM 事件：

   ~~~html
   <button v-on:click="doSomeThing" class="btn btn-primary">按钮</button>
   ~~~

2. 修饰符

   修饰符（Modifiers）是以半角句号 `.` 指明的特殊后缀，用于指出一个指令应该以特殊方式绑定。例如，`.prevent` 修饰符告诉 `v-on` 指令对于触发的事件调用 `event.preventDefault()`：

   ~~~html
   <form v-on:submit.prevent="onSubmit">
        <input type="text" name="name" value="" />
        <input type="submit"  value="提交" />
   </form>
   ~~~

   当我点击提交后，会先执行`onSubmit`方法。

### 过滤器

过滤器可以用在两个地方：**mustache 插值和 v-bind 表达式**。过滤器应该被添加在 JavaScript 表达式的尾部，由“管道”符指示：

过滤器函数总**接受表达式的值作为第一个参数**。

~~~html
<p>
     {{value|capitalize}}
</p>
~~~

~~~javascript
new Vue({
  // ...
  //所有的过滤器都放在这里
  filters: {
    capitalize: function (value) {
      if (!value) return ''
      value = value.toString()
      return value.charAt(0).toUpperCase() + value.slice(1)
    }
  }
})
~~~

过滤器可以串联：

~~~html
        <p>
            {{value|capitalize|addChar}}
        </p>
~~~

`addChar`接受的值是`capitalize`处理过的

过滤器是 JavaScript 函数，因此可以接受参数：

```
{{ message | filterA('arg1', arg2) }}
```

这里，字符串 `arg1` 将传给过滤器作为第二个参数， `arg2` 表达式的值将被求值然后传给过滤器作为第三个参数。

### 缩写

#### `v-bind`缩写

~~~html
<!-- 完整语法 -->
<label v-bind:class="labelClass">标签</label>
<!-- 缩写 -->
<label :class="labelClass">标签</label>
~~~

#### `v-on`缩写

~~~html
<!-- 完整语法 -->
<button v-on:click="doSomeThing">按钮</button>
<!-- 缩写 -->
<button @click="doSomeThing">按钮</button>
~~~

## 计算属性

~~~html
<p>computed:{{addNumber}}</p>
~~~

~~~javascript

computed: {
            addNumber: function () {
                return this.labelId + 1;
            }
        }
~~~

当vue对象中的labelId改变了，对应的计算结果也会同步

**计算属性是基于它们的依赖进行缓存的**

计算属性不能有参数

## 组件

### 使用组件

#### 局部注册

```javascript
var Child = {
  template: '<div>A custom component!</div>'
}

new Vue({
  // ...
  components: {//组件放在components下
    // <my-component> 将只在父模板可用
    'my-component': Child  //这里必须是对象，不能是文字
  }
})
```

#### data必须是函数

~~~javascript
Vue.component('component-test', {
    template: '<div><button v-on:click="clickTimes+=1">click me：{{clickTimes}}</button></div>',
    data: function () {
        return {
            clickTimes: 1,
        };
    }
});
~~~

data中是组件需要的参数，类似vue对象的data，当组件第一次加载时会用data中的值

#### 构成组件

在vue中，父子组建的关系可以总结为**props down,events up**。父组件通过 **props** 向下传递数据给子组件，子组件通过 **events** 给父组件发送消息。

![](https://cn.vuejs.org/images/props-events.png)

### Prop

#### 使用Prop传递数据

父组件给子组件传递数据，子组件要显式地用 [`props` 选项](https://cn.vuejs.org/v2/api/#props)声明它期待获得的数据：

~~~javascript
Vue.component('comp-props', {
    props: ['promsg'],     //这里的参数不能使用‘-’连字符，一定要注意
    template: '<p>{{promsg}}</p>',
});
~~~

~~~html
<comp-props promsg="my comp-props"></comp-props>
<!--
因为这里使用的是静态的参数，所用直接使用promsg就行，不用v-bind绑定-->
~~~

传递多个参数

~~~javascript
    Vue.component('page', {
        props: ['pagelist','pageCount'],//这里是字符串数组
        template:'<p>{{pagelist}}+{{pageCount}}</p>',
    });
~~~

注意，这里的第二个参数`pageCount`使用了驼峰命名，在传值时应该这样传

~~~html
<page pageList="abcdefg"  page-count="10"></page>
~~~

将驼峰命名的换成用 kebab-case (短横线隔开式) 

#### 动态Prop

~~~javascript
<comp-props v-bind:promsg="message"></comp-props>
<!--这里的message就是vue对象中定义的值-->
~~~

#### 单向数据流

prop 是单向绑定的：当父组件的属性变化时，将传导给子组件，但是不会反过来。

另外，每次父组件更新时，子组件的所有 prop 都会更新为最新值。这意味着你**不应该**在子组件内部改变 prop。

~~~javascript
    Vue.component('comp-single-track', {
        props: ['params'],
        template: '<p>{{addChar}}</p>',
        //data: function () {
        //    return { dataparam:this.params+'-data-param' }
        //},
        computed: {
            addChar: function () {
                return this.params + '-data-param';
            }
        }
    });
//当我使用data时，params的值发生变化，组件显示不变
//我使用计算属性时，发生变化
~~~

> 注意在 JavaScript 中对象和数组是引用类型，指向同一个内存空间，如果 prop 是一个对象或数组，在子组件内部改变它**会影响**父组件的状态。

#### Prop验证

~~~javascript
Vue.component('comp-validate', {
    props: {
      // 基础类型检测 (`null` 意思是任何类型都可以)
        propa: Number,
      // 多种类型
        propb: [String, Number],
      // 必传且是字符串
        propc: {
            type: String,
            required: false,
        },
       // 数字，有默认值
        propd: {
            type: Number,
            default:100
        },
      // 数组/对象的默认值应当由一个工厂函数返回
        prope: {
            type: Object,
            default: function () {
                return {msg:'Hello World!'};
            }
        },
       // 自定义验证函数，验证不通过时会报异常
        propf: {
            validator: function (value) {
                return value > 10;
            }
        }
    },
    template:'<p>prop验证{{propd}}||{{prope.msg}}||{{propf}}</p>',
});
~~~

## Class与Style绑定

### 绑定HTML Class

~~~html
<div v-bind:class="{active:isActive}">
    <h2>测试对象绑定</h2>
</div>
<div class="static" v-bind:class="{active:isActive,'text-danger':hasError}">
    <h2>测试同时存在</h2>
</div>

<div v-bind:class="classObj">
    <h2>测试使用data中的对象</h2>
</div>

<div v-bind:class="classObject">
    <h2>测试使用计算属性</h2>
</div>
~~~

注意上面的`'text-danger'`，在class中间有`-`时需要用单引号引起来。

~~~javascript
var vm = new Vue({
    el: '#app',
    data: {
        classObj: {
            container: true,
            'col-lg-6': true,  //样式中有-，所有要用单引号
        },
        title: 'Class 与 Style 绑定',
        isActive: true,
        hasError: false,
        classType: 2,
    },
    computed: {
        classObject: function () {
            return {
                hide: this.classType === 1,

            };
        }
    }
});
~~~

