﻿using UnityEngine;
using System.Collections;

public class MapCreator : MonoBehaviour {
	public int				MAP_SIZE_X = 7;		// マップ横幅 (偶数指定の場合は、自動的に奇数にされる)
	public int				MAP_SIZE_Z = 10;	// マップ奥幅 (偶数指定の場合は、自動的に奇数にされる)
	public	GameObject		player;				// プレイヤーオブジェクト格納用
	private MapSize		size;					// マップサイズ型の変数
	private MapAxis		playerAxis;				// プレイヤー座標を扱うPlayerAxis型の変数
	private MapArrayBlock	mapBlock;			// 地面用MapArrayBlock型の変数
	private MapArrayFloor	mapFloor;			// 地上用MapArrayFloor型の変数
	public  GameObject[]    bossPrefab;
	private GameObject[]	tagObjects;
	public	GameObject[]	prefab_BL;			// 床ブロック格納用のプレファブ配列
	public	GameObject[]	prefab_WALL;		// 壁ブロック格納用のプレファブ配列
	public	GameObject[]	prefab_enemy;		// 敵の格納用のプレファブ配列
	public	GameObject[]	prefab_BreakBlock;
	private float timer = 0.0f;
	private float interval = 2.0f;
	public GameObject[] 	Prefab_Player;

	// ■■■
	void Start(){

		//player		= GameObject.FindGameObjectWithTag("Player");						// プレイヤーオブジェクト格納
		player = Instantiate(Prefab_Player[GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo]);
		size		= new MapSize(MAP_SIZE_X , MAP_SIZE_Z);								// MapSizeクラスのインスタンス生成
		playerAxis	= new MapAxis(player , size , prefab_BL[0].transform.localScale);	// PlayerAxisクラスのインスタンス生成
		mapBlock	= new MapArrayBlock(prefab_BL , "BL" , size , playerAxis);			// 地面用MapArrayクラスのインスタンス生成
		mapFloor	= new MapArrayFloor("FL" , size , playerAxis);						// 地上用MapArrayクラスのインスタンス生成
		
		mapFloor.setWall(prefab_WALL);					// 壁オブジェクトの渡す
		mapFloor.setObstacle(prefab_BreakBlock);		// 障害物オブジェクトを渡す
		mapFloor.setEnemy(prefab_enemy);				// 敵オブジェクトを渡す
		initialize();									// プレイヤー位置／マップ初期化
		StartCoroutine("enemyEmitter" , 1.0f);			// 敵出現用のコルーチン開始
	}

	// ■■■プレイヤー位置／マップ初期化■■■
	private void initialize(){
		playerAxis.initialize();						// プレイヤーの初期座標を設定 (マップ中央座標)
		mapBlock.startMap_Create();						// スタート時のマップ(床)作成
		mapFloor.startMap_Create();						// スタート時のマップ(地上)作成
	}
	
	// ■■■■■■
	void Update(){
		playerAxis.updateAxis ();						// プレイヤー座標を更新
		mapBlock.renewal ();								// マップ(床)の更新
		mapFloor.renewal ();								// マップ(地上)の更新
		
		timer += Time.deltaTime;
		if (timer >= interval) {
			Check ("GreenSphere");
			timer = 0;
		}
	
	}

	void Check(string tagname){
		tagObjects = GameObject.FindGameObjectsWithTag(tagname);
	}

	// ■■■敵出現用のコルーチン■■■
	IEnumerator enemyEmitter(float time){
		while(true){
			mapFloor.enemyArrival();					// 敵出現処理
			yield return new WaitForSeconds(time);		// time秒、処理を待機.
		}
	}
}