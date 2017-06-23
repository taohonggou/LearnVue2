# 学习Vue笔记

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