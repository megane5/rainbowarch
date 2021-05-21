
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {
    Rigidbody2D rb_;
    Animator animator_;
    public GameObject torch_;
    TorchManage torchManage_;
    public GameObject ivy_;
    IvyManage ivyManage_;
    public AudioClip jumpSound_;
    [SerializeField]
    private GameObject icon;

    private float jumpForce_ = 7f;       // ジャンプ時に加える力
    private float jumpThreshold_ = 1.0f;    // ジャンプ中か判定するための閾値
    private int direction_ = 1;
    private int directionY_ = 1;
    private float walkForce_ = 0.7f;       // 走り始めに加える力
    private float walkSpeed_ = 0.5f;       // 走っている間の速度
    private float dashForce_ = 1.0f;       // 走り始めに加える力
    private float dashSpeed_ = 1.0f;
    private float climbSpeed_ = 0.5f;
    private float walkThreshold_ = 0.5f;
    private float stateEffect_ = 1;       // 状態に応じて横移動速度を変えるための係数
    private bool jump_ = false;
    private bool directionYClicked_ = false;
    private bool onGround_ = true;
    private bool climbable_ = false;
    private bool climb_ =false;
    private bool stop_ = false;
    private bool dash_=false;
    
    WallCollision rightCollision_;
    WallCollision leftCollision_;
    JumpDicision jumpDicision_;
    ClimbDicision climbDicision_;
    //FallDicision fallDicision_;
    string state_;
    string prevState_;
    AudioSource[] BGMs;


    GameObject colGameObject_;


    private bool transFlag_;
    private string Color_ = "PEACH";
    private string transColor_;
    private string moveGimmick_="NONE";
    private string orbColor_="NONE";
    public int endcount { get; set; }
    List<string> coloredList;

    // Use this for initialization
    void Start()
    {
        endcount = 0;
        coloredList = new List<string>();
        rightCollision_ = gameObject.transform.Find("Collider Right").gameObject.GetComponent<WallCollision>();
        leftCollision_ = gameObject.transform.Find("Collider Left").gameObject.GetComponent<WallCollision>();
        jumpDicision_ = gameObject.transform.Find("Collider for jump").gameObject.GetComponent<JumpDicision>();
        climbDicision_ = gameObject.transform.Find("Collider for ivy").gameObject.GetComponent<ClimbDicision>();
        //fallDicision_ = gameObject.transform.Find("Collider for fall").gameObject.GetComponent<FallDicision>();
        rb_ = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
        torchManage_ = torch_.GetComponent<TorchManage>();
        ivyManage_ = ivy_.GetComponent<IvyManage>();
        BGMs =new AudioSource[] { GameObject.Find("BGM1").GetComponent<AudioSource>(), GameObject.Find("BGM2").GetComponent<AudioSource>(), GameObject.Find("BGM3").GetComponent<AudioSource>() };
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale != 0f)
        {
            Key();          // ① 入力を取得
            ChangeState();          // ② 状態を変更する
            ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
            Gimmick();
            Move();                 // ④ 入力に応じて移動する
            if (endcount > 0)
            {
                endcount++;
                if (Input.GetKeyDown(KeyCode.Return) && endcount > 10)
                {
                    SceneManager.LoadScene("title");
                }
            }
        }
    }

    private void Move()
    {
        rb_.isKinematic = false;
        if (!climb_||onGround_)
        {
            float speedX = Mathf.Abs(rb_.velocity.x);
            //Debug.Log(direction_);
            //Debug.Log(rightCollision_.GetOnWall());
            if (!(direction_ == 1 && rightCollision_.GetOnWall()) && !(direction_ == -1 && leftCollision_.GetOnWall()))
            {
                if (speedX < this.walkSpeed_)
                {
                    rb_.AddForce(transform.right * direction_ * walkForce_ * stateEffect_); //未入力の場合は key の値が0になるため移動しない

                    //Debug.Log(speedX);
                }
                else
                {
                    this.transform.position += new Vector3(walkSpeed_ * Time.deltaTime * direction_ * stateEffect_, 0, 0);

                }
                /***
                if (!dash_)
                {
                    
                }
                else
                {
                    if (speedX < this.dashSpeed_)
                    {
                        rb_.AddForce(transform.right * direction_ * dashForce_ * stateEffect_); //未入力の場合は key の値が0になるため移動しない

                        //Debug.Log(speedX);
                    }
                    else
                    {
                        this.transform.position += new Vector3(dashSpeed_ * Time.deltaTime * direction_ * stateEffect_, 0, 0);

                    }
                }
                ***/
            }
           
        }
        
        if (climb_&&!jump_)
        {
            if (!stop_)
            {
                if (!onGround_)
                {
                    rb_.velocity = new Vector3(0, climbSpeed_ * directionY_, 0);
                    rb_.isKinematic = true;
                }
                else
                {
                    rb_.velocity = new Vector3(rb_.velocity.x, climbSpeed_ * directionY_, 0);

                }
            }
            else
            {
                if (directionY_ >= 0)
                {
                    rb_.velocity = new Vector3(0, 0, 0);
                    rb_.isKinematic = true;
                }
                else
                {
                    rb_.velocity = new Vector3(0, climbSpeed_ * directionY_, 0);
                    rb_.isKinematic = true;
                }

            }
            //Debug.Log(speedX);
        }
        if (onGround_ || (climbable_&&climb_))
        {
            if (jump_)
            {
                //Debug.Log("jump");
                print(GetComponent<AudioSource>());
                GetComponent<AudioSource>().PlayOneShot(jumpSound_);
                rb_.AddForce(transform.up * this.jumpForce_);
                onGround_ = false;
            }

        }
        if (climbable_ && directionYClicked_)
        {
            climb_ = true;
        }
        if (!climbable_ || jump_ || (onGround_&&direction_!=0))
        {
            climb_ = false;
        }
    }

    private void ChangeAnimation()
    {
        if (direction_ == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (direction_ == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }   
        if (prevState_ != state_)
        {
            animator_.SetBool(state_, true);
            animator_.SetBool(prevState_, false);
        }
        if (transFlag_ && transColor_ != "NONE" && (state_ == "STAND") && transColor_!=Color_&&moveGimmick_=="NONE")
        {
            //Debug.Log(transColor_);
            animator_.SetBool(transColor_, true);
            animator_.SetBool(Color_, false);
            Color_ = transColor_;
        }
        prevState_ = state_;
    }

    private void ChangeState()
    {
        if(jumpDicision_)
            onGround_ = jumpDicision_.GetOnGround();
        
        if(climbDicision_)
        climbable_ = climbDicision_.GetOnLadders();
        //Debug.Log(climbable_);
        if (Mathf.Abs(rb_.velocity.y) > jumpThreshold_)
        {
            onGround_ = false;
        }
        if (climb_)
        {
            state_ = "CLIBM";
        }
        
        // 接地している場合
        else if (onGround_)
        {
            // 走行中
            if (direction_ != 0)
            {
                state_ = "WALK";
                //待機状態
            }
            else
            {
                state_ = "STAND";
            }
            // 空中にいる場合
        }
        else
        {
            // 上昇中
            if (!onGround_&&rb_.velocity.y > 0)
            {
                state_ = "JUMP";
                // 下降中
            }
            else if (!onGround_ && rb_.velocity.y < 0)
            {
                state_ = "FALL";
            }
        }

        if (state_ == "STAND" && transFlag_)
        {
        }
        //Debug.Log(state_);
    }

    private void Key()
    {
        if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
        {
            transFlag_ = true;
        }
        else
        {
            transFlag_ = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction_ = 1;//右方向へ移動
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction_ = -1;//左方向へ移動
        }
        else
        {
            direction_ = 0;//移動しない
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            directionYClicked_ = true;
        }
        else
        {
            directionYClicked_ = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dash_ = true;
        }
        else
        {
            dash_ = false;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            directionY_ = 1;//上方向へ移動
        }
        else if ( Input.GetKey(KeyCode.DownArrow))
        {
            directionY_ = -1;//下方向へ移動
        }
        else
        {
            directionY_ = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump_ = true;
        }
        else
        {
            jump_ = false;
        }
    }
    private void Gimmick()
    {
        if (moveGimmick_ == "TORCH" && transFlag_&&(state_=="STAND"||state_=="WALK"))
        {
            colGameObject_.GetComponent<TorchManage>().setFire();
        }
        if (moveGimmick_ == "IVY" && transFlag_ && (state_ == "STAND" || state_ == "WALK"))
        {
            colGameObject_.GetComponent<IvyManage>().grow();
        }
        if (moveGimmick_ == "FALL" && transFlag_&&Color_=="BLUE" && (state_ == "STAND" || state_ == "WALK"))
        {
            colGameObject_.GetComponent<FallManage>().Delete();
            icon.GetComponent<IconManage>().Delete();
            moveGimmick_ = "NONE";
        }
        if (orbColor_!="NONE" && transFlag_)
        {
            bool colored=false;
            foreach(string s in coloredList)
            {
                if (orbColor_ == s)
                {
                    colored = true;
                    break;
                }
            }
            if (!colored)
            {
                GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
                GameObject[] backs = GameObject.FindGameObjectsWithTag("Back");
                foreach (GameObject obj in grounds)
                {
                    if (orbColor_ == obj.GetComponent<BackColorManage>().color || orbColor_ == obj.GetComponent<BackColorManage>().color2)
                    {
                        obj.GetComponent<BackColorManage>().coloring();
                    }
                }
                foreach (GameObject obj in backs)
                {
                    if (orbColor_ == obj.GetComponent<BackColorManage>().color || orbColor_ == obj.GetComponent<BackColorManage>().color2)
                    {
                        obj.GetComponent<BackColorManage>().coloring();
                    }
                }
                colGameObject_.GetComponent<OrbManage>().Delete();
                Debug.Log(coloredList.Count);
                if (coloredList.Count != 2)
                {
                    BGMs[coloredList.Count + 1].time = BGMs[coloredList.Count].time;
                    BGMs[coloredList.Count].Stop();

                    BGMs[coloredList.Count + 1].Play();
                }
                else
                {//クリア時処理
                    endcount++;
                }
                coloredList.Add(orbColor_);
            }
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if ((col.gameObject.name == "IVY" && Color_ == "GREEN") || (col.gameObject.name == "TORCH" && Color_ == "RED") || (col.gameObject.name == "FALL" && Color_ == "BLUE")
                  )
        {
            moveGimmick_ = "NONE";
            icon.GetComponent<IconManage>().Delete();
            //Debug.Log(col.gameObject.name + "NONE");
        }
        if (col.gameObject.name == "BLUEORB" || col.gameObject.name == "GREENORB" || col.gameObject.name == "REDORB")
        {
            orbColor_ = "NONE";
            icon.GetComponent<IconManage>().Delete();
        }
        /*if (col.gameObject.tag == "Ground")
        {
            if (!onGround_)
                onGround_ = true;
        }
       */
        //Debug.Log(onGround_);
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if ((col.gameObject.name == "IVY" && Color_ == "GREEN") || (col.gameObject.name == "TORCH" && Color_ == "RED") || (col.gameObject.name == "FALL" && Color_ == "BLUE"))
        {

            moveGimmick_ = col.gameObject.name;
            colGameObject_ = col.gameObject;
            icon.GetComponent<IconManage>().Drow(colGameObject_.transform.position);

            //Debug.Log(col.gameObject.name);
        }
        if (col.gameObject.name == "BLUEORB" || col.gameObject.name == "GREENORB" || col.gameObject.name == "REDORB")
        {
            orbColor_ = col.gameObject.name.Substring(0, col.gameObject.name.Length - 3);
            colGameObject_ = col.gameObject;
            icon.GetComponent<IconManage>().Drow(colGameObject_.transform.position + new Vector3(0.18f, 0.18f));
        }
        /*if (col.gameObject.tag == "Ground")
        {
            if (!onGround_)
                onGround_ = true;
        }
        else
        {
            onGround_ = false;
        }*/
        // Debug.Log(onGround_);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.name == "BLUE" || col.gameObject.name == "RED" || col.gameObject.name == "GREEN"))
        {
            transColor_ = "NONE";
            //Debug.Log(col.gameObject.name + "NONE");
            icon.GetComponent<IconManage>().Delete();
        }
        else
        {
            if ((col.gameObject.name == "IVY" && Color_ == "GREEN") ||
                (col.gameObject.name == "TORCH" && Color_ == "RED"))
            {
                moveGimmick_ = "NONE";
                icon.GetComponent<IconManage>().Delete();
                //Debug.Log(col.gameObject.name + "NONE");
            }
            else if (col.gameObject.name == "BLUEORB" || col.gameObject.name == "GREENORB" || col.gameObject.name == "REDORB")
            {
                orbColor_ = "NONE";
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if ((col.gameObject.name == "BLUE" || col.gameObject.name == "RED" || col.gameObject.name == "GREEN")&&Color_!= col.gameObject.name)
        {
            transColor_ = col.gameObject.name;
            colGameObject_ = col.gameObject;
            icon.GetComponent<IconManage>().Drow(colGameObject_.transform.position + new Vector3(0.18f, 0.18f));
        }
        else
        {
            if ((col.gameObject.name == "IVY" && Color_ == "GREEN"&& !col.gameObject.GetComponent<IvyManage>().grown_) || 
                (col.gameObject.name == "TORCH" && Color_ == "RED" && !col.gameObject.GetComponent<TorchManage>().onFire_))
            {

                moveGimmick_ = col.gameObject.name;
                colGameObject_ = col.gameObject;
                float a = 0;
                if (col.gameObject.name == "IVY") a = -0.23f;
                icon.GetComponent<IconManage>().Drow(colGameObject_.transform.position + new Vector3(0.18f, 0.18f+a));
                //Debug.Log(col.gameObject.name);
            }
            else if(col.gameObject.name=="BLUEORB"|| col.gameObject.name == "GREENORB"|| col.gameObject.name == "REDORB")
            {
                orbColor_ = col.gameObject.name.Substring(0, col.gameObject.name.Length-3);
                colGameObject_ = col.gameObject;
            }
            if (col.gameObject.name == "IVY" && col.gameObject.GetComponent<IvyManage>().grown_){
                Debug.Log(stop_);
                stop_ = col.gameObject.GetComponent<IvyManage>().stop_;
            }
        }

       
    }
}
