# Health Bar
视频地址：https://v.youku.com/v_show/id_XNDQ0MDIzMTkyOA==.html?spm=a2h3j.8428770.3416059.1  

优缺点对比：
   
| | IMGUI | UGUI |  
|-|-------|------|  
| 优点 | 开发简单，仅需几行代码 | 有锚点，更方便屏幕自适应 |  
| 缺点| 需要在将3D位置映射到屏幕位置后，如果对结果进行加减，会使得血条位置偏移过多 | 如果人物过多,需要太多的canvas |  

预制的使用方法：
1. 添加游戏对象。
2. 将UIScript和IMGUI(Script)挂载到该游戏对象上。
3. 选择该游戏对象，右键菜单 -> UI -> Canvas, 添加画布子对象。
4. 在游戏对象的Inspector视图中设置UIScript的Health Prefab(拖入预设)和Canvas(拖入上一步添加的Canvas)。
