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

   组件的位置实在创建vue对象前面，使用代码如下：

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

   ​