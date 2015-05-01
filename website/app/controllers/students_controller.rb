class StudentsController < ApplicationController
	def show
    @user = Student.find(params[:id])
  end


  def new
    @user = Student.new
  end


  def take_quiz
    @no=params[:Q]
    @que=Question.where(subject_id: params[:subject_id])
    @questions=@que.first(@no.to_i)
    @records= @questions.map(&:id)
  end


  def Answer_quiz
     @que_np=params[:Q_no]
     @records=params[:my_records].split(/,/)

     @records.each  do |e|
      
      x=Answer.new
      x.student_id=current_student.id
      x.question_id=e.to_i
      symbol=e.to_sym
      x.ans=params[symbol]

      if  x.ans==Question.find(e.to_i).correct_answer
        x.correct=true
      else 
        x.correct=false

      end
      x.save
        end
      redirect_to view_courses_students_path
  end  
   

  def view_courses
   @subjects = Subject.where(school: current_student.school)
  end


  def view_course_teachers
    #get ids of teachers teaching in this school @teachers
    #@t=Teacher.where(school: current_student.school).pluck(:id)
    #get the ides of teachers teaching this subject
    @t=TeacherRequestSubject.where( verified: true ,subject_id: params[:subject_id]).pluck(:teacher_id)
    @t=Teacher.where(id:@t)
    @teachers=Teacher.where(verified:true)
  end


  def send_message
    @message=Mess.new
    @var=params[:teacher_id]
  end 


  def submit 
      @message=Mess.new
      @message.semail=current_student.email
      @message.remail=Teacher.find(params[:teacher_id]).email
      @message.text=params[:my_input]

      @message.save
      #redirect_to view_courses_students_path
      redirect_to view_teachers_in_school_students_path
  end 

  
  def show_course
   @subject = Subject.find(params[:subject_id])
  end


  def create
    @user = Student.new(user_params)
    if @user.save
      log_in @user
      flash[:success] = "Welcome!"
      redirect_to("home#index")
    else
      render 'new'
    end
  end

   def view_contacts
  @user=current_student
  @s= Student.where(school:current_student.school)
  @students=@s.where.not(id:@user.id)


  end

  def view_messages
    @user=current_student
    @sender=Student.find(params[:student_id]) 
    @message=Mess.where(semail:@sender.email)

  end

  def view_messages_teacher
    @user=current_student
    @sender=Teacher.find(params[:teacher_id]) 
    @message=Mess.where(semail:@sender.email)

  end


  def view_teachers_in_school
    if current_student.verified
      @teachers = Teacher.where(school: current_student.school, verified: true)
    else
      @teachers = {}
    end
  end


  def view_teacher_messages
     @user=current_student
    @sender=Teacher.find(params[:teacher_id]) 
    @message=Mess.where("((semail = ? AND remail = ?) OR(semail = ? AND remail = ?))" ,
      @sender.email, current_student.email, current_student.email, 
      @sender.email).order(created_at: :asc)
    #form
     @new_message=Mess.new
    @var=params[:teacher_id]
  end


  private
    def user_params
      params.require(:student).permit(:email, :password,
                                   :password_confirmation)
    end
end
